using Godot;

namespace Components
{
	public partial class PivotOrigin : Node2D
	{
		[Export]
		public bool Active { get; set; } = true;

		[Export]
		private float minRotation;
		[Export]
		private float maxRotation;

		private Node2D target;

		public void SetTarget(Node2D _target)
		{
			target = _target;
		}

		public void ClearTarget()
		{
			target = null;
		}

		public override void _Process(double delta)
		{
			if (!Active) return;

			if (target != null)
			{
				LookAt(target.GlobalPosition);
				RotationDegrees = Mathf.Clamp(RotationDegrees, minRotation, maxRotation);
			}
			else
			{
				RotationDegrees = Mathf.Lerp(RotationDegrees, 0.0f, (float)delta * 20);
			}
		}
	}
}