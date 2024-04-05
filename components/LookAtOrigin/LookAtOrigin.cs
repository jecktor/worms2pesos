using Godot;

namespace Components
{
	public partial class LookAtOrigin : Node2D
	{
		[Export]
		public bool Active { get; set; } = true;

		[Export]
		private float speed = 20.0f;

		[Export]
		private float maxDistance;

		private Vector2 prevTargetPos = Vector2.Zero;

		private Node2D origin;
		private Node2D target;

		public void SetTarget(Node2D _target)
		{
			target = _target;
		}

		public void ClearTarget()
		{
			target = null;
		}

		public override void _Ready()
		{
			origin = GetChild<Node2D>(0);
		}

		public override void _Process(double delta)
		{
			if (!Active) return;

			if (target != null)
			{
				Vector2 targetPos = ToLocal(target.GlobalPosition);

				if (targetPos == prevTargetPos) return;

				Vector2 lookDir = Vector2.Zero.DirectionTo(targetPos);

				origin.Position = origin.Position.Lerp(lookDir * Mathf.Min(targetPos.Length(), maxDistance), (float)delta * speed);

				prevTargetPos = targetPos;
			}
			else
			{
				origin.Position = origin.Position.Lerp(Vector2.Zero, (float)delta * speed);
			}
		}
	}
}