using Godot;

namespace Components
{
	public partial class RotateOnSlope : Node
	{
		[Export]
		private Node2D target;

		[Export]
		private float rotationSpeed = 20.0f;

		public override void _PhysicsProcess(double delta)
		{
			float normalX = (Owner as CharacterBody2D).GetFloorNormal().X;

			target.Rotation = Mathf.LerpAngle(target.Rotation, Mathf.Atan2(normalX, 1), (float)delta * rotationSpeed);
		}
	}
}