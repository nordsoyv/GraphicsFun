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
		private Vector3 velocity = new Vector3( 0);
		private Quaternion heading;
		private Vector3 sumAcceleration;
		private float acceleration = 0.5f;
		private float MaxSpeed = 8.0f;

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

		public void HandleEvent(IEnumerable<MouseEvent> events)
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
				

			}




		}


		public void HandleEvent(IEnumerable<KeyboardEvent> events)
		{

			IEnumerable<KeybuttonEvent> keybuttonEvents = events.OfType<KeybuttonEvent>();

			var wButton = keybuttonEvents.Where(e => e.Key == Keys.W).Select(e => e).FirstOrDefault();
			var sButton = keybuttonEvents.Where(e => e.Key == Keys.S).Select(e => e).FirstOrDefault();
			var aButton = keybuttonEvents.Where(e => e.Key == Keys.A).Select(e => e).FirstOrDefault();
			var dButton = keybuttonEvents.Where(e => e.Key == Keys.D).Select(e => e).FirstOrDefault();
			var spaceButton = keybuttonEvents.Where(e => e.Key == Keys.Space).Select(e => e).FirstOrDefault();
			var altButton = keybuttonEvents.Where(e => e.Key == Keys.LeftAlt).Select(e => e).FirstOrDefault();

			sumAcceleration = Vector3.Zero;

			if (wButton != null)
			{
				wButton.Handled = true;
				MoveZ(1f);
			}
			if (sButton != null)
			{
				sButton.Handled = true;
				MoveZ(-1f);
			}
			if (aButton != null)
			{
				aButton.Handled = true;
				MoveX(1f);
			}
			if (dButton != null)
			{
				dButton.Handled = true;
				MoveX(-1f);
			}
			if (spaceButton != null)
			{
				spaceButton.Handled = true;
				MoveY(1f);
			}
			if (altButton != null)
			{
				altButton.Handled = true;
				MoveY(-1f);
			}

			if(sumAcceleration.Length()> 0.01f)
			{
				sumAcceleration.Normalize();
				sumAcceleration *= acceleration;
			}

		}

		private void MoveX(float amount)
		{
			
			var r = Vector3.Transform(new Vector3(amount, 0.0f, 0.0f), Heading);
			r.Normalize();
			sumAcceleration += r;
//			Position += Vector3.Transform(new Vector3(amount, 0.0f, 0.0f), Heading);
		}


		private void MoveY(float amount)
		{
			sumAcceleration += new Vector3(0, amount, 0);
//			Position += new Vector3(0, amount, 0);
		}


		private void MoveZ(float amount)
		{
			var r = Vector3.Transform(new Vector3(0.0f, 0.0f, amount), Heading);
			r.Normalize();
			sumAcceleration += r;
//			Position += Vector3.Transform(new Vector3(0.0f, 0.0f, amount), Heading);
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


		public void Update(GameTime gameTime)
		{
			var delta = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
			if (sumAcceleration.Length() < 0.01f)
			{
				
				if (velocity.Length() < 0.1f)
				{
					velocity = Vector3.Zero;
					return;
				}

				sumAcceleration = velocity;
				sumAcceleration.Normalize();
				sumAcceleration *= -acceleration * 10 ; // slow down a bit faster than normal accel
  
			}
			velocity += sumAcceleration * delta ;
			if(velocity.Length() >= MaxSpeed || velocity.Length() <= -MaxSpeed)
			{
				velocity.Normalize();
				velocity *= MaxSpeed;
			}
			Move();
			sumAcceleration = Vector3.Zero;
			//Console.WriteLine("Vel:" + velocity.X + " , " + velocity.Y + " , " + velocity.Z);
		}


		private void Move()
		{
			Position += velocity;
		}
	}
}
