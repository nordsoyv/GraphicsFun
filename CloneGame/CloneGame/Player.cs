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


		public void HandleEvent(List<KeybuttonEvent> events)
		{
			var unhandledEvents = events.Where(e => e.Handled == false).Select(e =>e);

			var wButton = unhandledEvents.Where(e => e.Key == Keys.W).Select(e => e).FirstOrDefault();
			var sButton = unhandledEvents.Where(e => e.Key == Keys.S).Select(e => e).FirstOrDefault();
			var aButton = unhandledEvents.Where(e => e.Key == Keys.A).Select(e => e).FirstOrDefault();
			var dButton = unhandledEvents.Where(e => e.Key == Keys.D).Select(e => e).FirstOrDefault();
			var spaceButton = unhandledEvents.Where(e => e.Key == Keys.Space).Select(e => e).FirstOrDefault();
			var altButton = unhandledEvents.Where(e => e.Key == Keys.LeftAlt).Select(e => e).FirstOrDefault();

			if (wButton != null)
			{
				wButton.Handled = true;
				MoveZ(0.1f);
			}
			if (sButton != null)
			{
				sButton.Handled = true;
				MoveZ(-0.1f);
			}
			if (aButton != null)
			{
				aButton.Handled = true;
				MoveX(0.1f);
			}
			if (dButton != null)
			{
				dButton.Handled = true;
				MoveX(-0.1f);
			}
			if (spaceButton != null)
			{
				spaceButton.Handled = true;
				MoveY(0.1f);
			}
			if (altButton != null)
			{
				altButton.Handled = true;
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
