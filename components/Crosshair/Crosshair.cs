using Godot;

namespace Components
{
	public partial class Crosshair : Node2D
	{
		public Node2D Center;

		public bool Aiming { get; private set; } = false;

		private Node2D root;
		private AnimationPlayer anim;

		private float minRotation = -90.0f;
		private float maxRotation = 90.0f;

		public void Aim()
		{
			Aiming = true;
			anim.Play("aim");
		}

		public void Holster()
		{
			Aiming = false;
			anim.Play("holster");
		}

		public override void _Ready()
		{
			Center = GetNode<Node2D>("Root/Center");

			root = GetNode<Node2D>("Root");
			anim = GetNode<AnimationPlayer>("Anim");
		}

		public override void _Process(double delta)
		{
			if (!Aiming) return;

			float targetRotation = root.RotationDegrees + Input.GetActionStrength("aim_down") - Input.GetActionStrength("aim_up");

			root.RotationDegrees = Mathf.Clamp(targetRotation, minRotation, maxRotation);
		}
	}
}