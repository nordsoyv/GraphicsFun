using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloneGame.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CloneGame
{
	public delegate void PlayerInfoChangedHandler(object sender, PlayerInfoChangedHandlerEventArgs e);

	public class PlayerInfoChangedHandlerEventArgs : EventArgs
	{
		public Vector3 Position { get; set; }
		public Quaternion Heading { get; set; }
	}

	class Player : IKeyboardEventReciver, IMouseEventReciver
	{
		private Vector3 position;
		private Quaternion heading;


		private float yaw;
		private float pitch;

		public Vector3 Position
		{
			get { return position; }
			set
			{
				position = value;
				PlayerInfoChangedHandlerEventArgs e = new PlayerInfoChangedHandlerEventArgs();
				e.Heading = heading;
				e.Position = position;
				OnChanged(e);
			}
		}
		public Quaternion Heading
		{
			get { return heading; }
			set
			{
				heading = value;
				PlayerInfoChangedHandlerEventArgs e = new PlayerInfoChangedHandlerEventArgs();
				e.Heading = heading;
				e.Position = position;
				OnChanged(e);
			}
		}

		public event PlayerInfoChangedHandler Changed;


		// Invoke the Changed event; called whenever player changes
		protected virtual void OnChanged(PlayerInfoChangedHandlerEventArgs e)
		{
			if (Changed != null)
				Changed(this, e);
		}

		public void HandleEvent(List<MouseEvent> events)
		{
			var movement = events.Where(e => e.EventType == MouseEvent.MouseEventType.Movement).Select(e => e);
			if (movement.Count() > 0)
			{

				if (movement.First().XMovement != 0)
				{
					RotateX(movement.First().XMovement * -0.005f);
				}

				if (movement.First().YMovement != 0)
				{
					RotateZ(movement.First().YMovement * -0.005f);
				}

				Heading = Quaternion.CreateFromYawPitchRoll(yaw, pitch, 0);
				Heading.Normalize();
				events.Remove(movement.First());

			}




		}


		public void HandleEvent(List<KeyboardEvent> events)
		{
			var wButton = events.Where(e => e.Key == Keys.W).Select(e => e);
			var sButton = events.Where(e => e.Key == Keys.S).Select(e => e);
			var aButton = events.Where(e => e.Key == Keys.A).Select(e => e);
			var dButton = events.Where(e => e.Key == Keys.D).Select(e => e);
			var spaceButton = events.Where(e => e.Key == Keys.Space).Select(e => e);
			var altButton = events.Where(e => e.Key == Keys.LeftAlt).Select(e => e);

			if (wButton.Count() > 0)
			{
				events.Remove(wButton.First());
				MoveZ(0.1f);
			}
			if (sButton.Count() > 0)
			{
				events.Remove(sButton.First());
				MoveZ(-0.1f);
			}
			if (aButton.Count() > 0)
			{
				events.Remove(aButton.First());
				MoveX(0.1f);
			}
			if (dButton.Count() > 0)
			{
				events.Remove(dButton.First());
				MoveX(-0.1f);
			}
			if (spaceButton.Count() > 0)
			{
				events.Remove(spaceButton.First());
				MoveY(0.1f);
			}
			if (altButton.Count() > 0)
			{
				events.Remove(altButton.First());
				MoveY(-0.1f);
			}


		}

		private void MoveX(float amount)
		{
			Position += Vector3.Transform(new Vector3(amount, 0.0f, 0.0f), Heading);
		}


		private void MoveY(float amount)
		{
			Position += new Vector3(0, amount, 0);
		}


		private void MoveZ(float amount)
		{
			Position += Vector3.Transform(new Vector3(0.0f, 0.0f, amount), Heading);
		}

		private void RotateZ(float xrmod)
		{
			pitch += xrmod;
			if (pitch > MathHelper.PiOver2) pitch = MathHelper.PiOver2;
			if (pitch < -MathHelper.PiOver2) pitch = -MathHelper.PiOver2;
		}

		private void RotateX(float zrmod)
		{

			yaw += zrmod;
			yaw = yaw % MathHelper.TwoPi;
		}






	}
}
