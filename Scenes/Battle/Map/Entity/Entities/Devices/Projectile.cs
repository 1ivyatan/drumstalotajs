using Godot;
using System;

public partial class Projectile : Area2D
{
	[Signal]
	public delegate void ProjectileLandedEventHandler();
	
	private ProjectileMotion projectileMotion;
	
	private TileMapLayer groundLayer;
	private Tween tween;
	
	public void SetTrajectory(float azimuth, float initialVelocity, float angle, Vector2 spawnPosition)
	{
		this.projectileMotion = new ProjectileMotion(spawnPosition, (double)azimuth, (double)angle, (double)initialVelocity);

		this.Position = this.projectileMotion.MapMovement.StartPosition;
		this.Rotation = (float)this.projectileMotion.MapMovement.Rotation;
	}
	
	public void Fire()
	{
		this.tween = this.CreateTween();
		this.tween.SetProcessMode(0); /* physics process */
		this.tween.SetTrans((Tween.TransitionType)1);
		this.tween.TweenProperty(this, "position", this.projectileMotion.MapMovement.EndPosition, this.projectileMotion.Movement.Time);
		this.tween.TweenCallback(Callable.From(() => {
			this.EmitSignal(SignalName.ProjectileLanded);
			this.QueueFree();
		}));
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2I position = this.groundLayer.LocalToMap(this.Position);
		TileData data = this.groundLayer.GetCellTileData(position);
		
		var height = data.GetCustomData("height");
		GD.Print(height);
	}
	
	public override void _Ready()
	{
		/* !!!!!!!!! */
		this.groundLayer = this.GetNode<TileMapLayer>("../../GroundLayer");
		GD.Print(this.projectileMotion.Movement.Range);
		Fire();
	}
}
