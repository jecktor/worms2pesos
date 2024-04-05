using Godot;

namespace Components
{
	public partial class MovementComponent : Node
	{
		[Export]
		private Node2D[] toFlip;
		[Export]
		private AnimationTree anim;

		[Export]
		public bool CanMove { get; set; } = true;

		[Export]
		private float speed = 125.0f;

		public float Direction { get; set; }
		public int FacingDirection { get; private set; } = 1;

		public override void _Process(double _delta)
		{
			Vector2 velocity = (Owner as CharacterBody2D).Velocity;

			if (CanMove && Direction != 0.0f)
			{
				FacingDirection = (int)Direction;

				foreach (Node2D node in toFlip)
					node.Scale = new Vector2(FacingDirection, 1);

				velocity.X = Direction * speed;

				if ((Owner as CharacterBody2D).IsOnWall())
				{
					anim.Set("parameters/conditions/idle", true);
					anim.Set("parameters/conditions/walking", false);
				}
				else
				{
					anim.Set("parameters/conditions/idle", false);
					anim.Set("parameters/conditions/walking", true);
				}
			}
			else
			{
				velocity.X = Mathf.MoveToward((Owner as CharacterBody2D).Velocity.X, 0, speed);

				anim.Set("parameters/conditions/idle", true);
				anim.Set("parameters/conditions/walking", false);
			}

			(Owner as CharacterBody2D).Velocity = velocity;
		}
	}
}