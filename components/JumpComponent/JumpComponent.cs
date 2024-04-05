using Godot;

namespace Components
{
	public partial class JumpComponent : Node
	{
		[Export]
		public bool CanJump { get; set; } = true;

		[Export]
		private float jumpVelocity = -500.0f;
		[Export]
		private float backflipVelocity = -650.0f;

		[Export]
		private float jumpAirVelocity = 350.0f;
		[Export]
		private float backflipAirVelocity = 250.0f;

		[Export]
		private MovementComponent movementComponent;
		[Export]
		private AnimationTree anim;

		public bool IsJumping { get; private set; }

		private bool jumpCondition = false;

		private Timer jumpTimer;

		private float airVelocity;

		private bool jumpedOnce;
		private bool firstTimeOnGround;

		public void Jump()
		{
			jumpCondition = true;
		}

		public override void _Ready()
		{
			jumpTimer = GetNode<Timer>("JumpTimer");
		}

		public override void _PhysicsProcess(double _delta)
		{
			if (!(Owner as CharacterBody2D).IsOnFloor())
			{
				firstTimeOnGround = false;

				(Owner as CharacterBody2D).Velocity = (Owner as CharacterBody2D).Velocity with { X = airVelocity };

				anim.Set("parameters/conditions/hit_ground", false);
			}
			else if (!firstTimeOnGround)
			{
				firstTimeOnGround = true;
				jumpedOnce = false;

				anim.Set("parameters/conditions/flip", false);
				anim.Set("parameters/conditions/jump", false);
				anim.Set("parameters/conditions/hit_ground", true);

				IsJumping = false;
			}

			if (!CanJump)
				jumpCondition = false;

			if (!jumpCondition) return;
			jumpCondition = false;

			IsJumping = true;

			if (!jumpedOnce)
			{
				jumpedOnce = true;

				jumpTimer.Start();

				(Owner as CharacterBody2D).Velocity = (Owner as CharacterBody2D).Velocity with { X = 0.0f };

				anim.Set("parameters/conditions/jump", true);
			}
			else if (!jumpTimer.IsStopped())
			{
				jumpTimer.Stop();

				airVelocity = backflipAirVelocity * -movementComponent.FacingDirection;
				(Owner as CharacterBody2D).Velocity = (Owner as CharacterBody2D).Velocity with { Y = backflipVelocity };

				anim.Set("parameters/conditions/flip", true);
			}
		}

		public void OnJumpTimerTimeout()
		{
			airVelocity = jumpAirVelocity * movementComponent.FacingDirection;
			(Owner as CharacterBody2D).Velocity = (Owner as CharacterBody2D).Velocity with { Y = jumpVelocity };
		}
	}
}