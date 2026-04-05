using Godot;
using System;

namespace Drumstalotajs.Scores;

public partial class Score : RefCounted
{
	private SceneTreeTimer _timer;
	private double _timeLimit;
	
	public double GetRemainingTime()
	{
		return 222;
	}
	
	public void Start()
	{
		
	}
	
	public Score (double timeLimit)
	{
		//_timer = ((SceneTree)Engine.GetMainLoop()).CreateTimer(timeLimit);
		_timeLimit = timeLimit;
	}
}
