using Godot;
using Components;

namespace Entity
{
	public partial class Worm : CharacterBody2D
	{
		private JumpComponent jumpComponent;
		private MovementComponent movementComponent;

		private LookAtOrigin lookAt;
		private PivotOrigin pivot;

		private Crosshair crosshair;

		public void Aim()
		{
			jumpComponent.CanJump = false;
			movementComponent.CanMove = false;

			crosshair.Aim();

			lookAt.SetTarget(crosshair.Center);
			pivot.SetTarget(crosshair.Center);
		}

		public void Holster()
		{
			jumpComponent.CanJump = true;
			movementComponent.CanMove = true;

			crosshair.Holster();

			lookAt.ClearTarget();
			pivot.ClearTarget();
		}

		public override void _Ready()
		{
			jumpComponent = GetNode<JumpComponent>("JumpComponent");
			movementComponent = GetNode<MovementComponent>("MovementComponent");

			lookAt = GetNode<LookAtOrigin>("body/head/PivotOrigin/LookAtOrigin");
			pivot = GetNode<PivotOrigin>("body/head/PivotOrigin");

			crosshair = GetNode<Crosshair>("Crosshair");
		}

		public override void _PhysicsProcess(double _delta)
		{
			if (Input.IsActionJustPressed("jump"))
				jumpComponent.Jump();

			if (Input.IsActionJustPressed("ui_focus_next"))
			{
				if (crosshair.Aiming)
					Holster();
				else
					Aim();
			}

			if (!crosshair.Aiming)
			{
				movementComponent.CanMove = !jumpComponent.IsJumping;
				movementComponent.Direction = Input.GetAxis("move_left", "move_right");
			}

			MoveAndSlide();
		}
	}
}