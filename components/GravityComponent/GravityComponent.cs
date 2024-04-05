using Godot;

namespace Components
{
	public partial class GravityComponent : Node
	{
		[Export]
		public bool Enabled { get; set; } = true;

		private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

		public override void _Process(double delta)
		{
			if (!Enabled || (Owner as CharacterBody2D).IsOnFloor()) return;

			(Owner as CharacterBody2D).Velocity = (Owner as CharacterBody2D).Velocity with { Y = (Owner as CharacterBody2D).Velocity.Y + gravity * (float)delta };
		}
	}

}