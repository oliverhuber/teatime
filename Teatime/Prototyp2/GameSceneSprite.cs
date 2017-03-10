using System;
using AudioToolbox;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public class GameSceneSprite : SKScene
	{
		// Prototype Dimension Mapping 
		public int Proto2Dim1 { get; set; }
		public int Proto2Dim2 { get; set; }
		public int Proto2Dim3 { get; set; }

		// Define Sprites and Labels 
		private SKSpriteNode infoSprite;
		private SKSpriteNode infoNode;
		private SKSpriteNode infoNode2;
		private SKSpriteNode infoNode3;
		private SKSpriteNode cancelSpark;
		private SKSpriteNode teatimeSprite;
		private SKSpriteNode container;

		private SKLabelNode infoLabel1;
		private SKLabelNode infoLabel2;
		private SKLabelNode infoLabel3;
		private SKLabelNode infoLabel4;
		private SKLabelNode infoLabel5;
		private SKLabelNode infoLabel6;
		private SKLabelNode infoLabel7;
		private SKLabelNode infoLabel8;
		private SKLabelNode infoLabel9;

		private SKEmitterNode particleEmitterNode;

		private bool switchInfo;
		private bool firstTouch;
		private bool infoTouch;
		private int blurFactor;

		protected GameSceneSprite(IntPtr handle) : base(handle)
		{
		}

		public GameSceneSprite()
		{
		}

		public override void DidMoveToView(SKView view)
		{
			switchInfo = false;
			Proto2Dim1 = 0;
			Proto2Dim2 = 0;
			Proto2Dim3 = 1;
			blurFactor = 1;

			// Setup Sprite Scene
			infoTouch = false;
			firstTouch = false;

			// Background gradient sprite node
			container = new SKSpriteNode("background");
			container.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2);
			container.Size = new CGSize(Frame.Width, Frame.Height);
			container.ZPosition = 0;
			AddChild(container);

			// Add Info Icon
			infoSprite = new SKSpriteNode("info");
			infoSprite.Position = new CGPoint(Frame.Width - 40, Frame.Height - 40);
			infoSprite.ZPosition = 10;
			infoSprite.XScale = 0.6f;
			infoSprite.YScale = 0.6f;
			infoSprite.Alpha = 0.8f;
			infoSprite.Name = "info";
			AddChild(infoSprite);

			// Add LongPress Gesture Recognizer and Handler
			var gestureLongRecognizer = new UILongPressGestureRecognizer(PressHandler);
			gestureLongRecognizer.MinimumPressDuration = 0.5;
			gestureLongRecognizer.AllowableMovement = 20f;
			View.AddGestureRecognizer(gestureLongRecognizer);

			// Start with the info text
			SetInfoText();

			// Create the Emitter Nodes
			CreateEmitterNode();
		}

		private void SetInfoText()
		{
			// Define and add Label 1
			infoLabel1 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Hier kannst du deine Stimmung erfassen",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 10)
			};
			infoLabel1.Alpha = 0.0f;
			infoLabel1.Name = "infoLabel";
			infoLabel1.ZPosition = 100;
			AddChild(infoLabel1);

			// Define and add Label 2
			infoLabel2 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 - 20)
			};
			infoLabel2.Alpha = 0.0f;
			infoLabel2.Name = "infoLabel";
			infoLabel2.ZPosition = 100;
			AddChild(infoLabel2);

			// Define and add Label 3
			infoLabel3 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Berühre den Bildschirm",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 10)
			};
			infoLabel3.Alpha = 0.0f;
			infoLabel3.ZPosition = 100;
			infoLabel3.Name = "infoLabel";
			AddChild(infoLabel3);

			// Define and add Label 4
			infoLabel4 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "und navigiere die Punkte",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 - 20)
			};
			infoLabel4.Alpha = 0.0f;
			infoLabel4.ZPosition = 100;
			infoLabel4.Name = "infoLabel";
			AddChild(infoLabel4);

			// Define and add Label 5
			infoLabel5 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "so dass diese schlussendlich",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 10)
			};
			infoLabel5.Alpha = 0.0f;
			infoLabel5.ZPosition = 100;
			infoLabel5.Name = "infoLabel";
			AddChild(infoLabel5);

			// Define and add Label 6
			infoLabel6 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "deiner aktuellen Gefühlslage entsprechen",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 - 20)
			};
			infoLabel6.Alpha = 0.0f;
			infoLabel6.Name = "infoLabel";
			infoLabel6.ZPosition = 100;
			AddChild(infoLabel6);

			// Define and add Label 7
			infoLabel7 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "wenn du die Punkte lange drückst",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 10)
			};
			infoLabel7.Alpha = 0.0f;
			infoLabel7.ZPosition = 100;
			infoLabel7.Name = "infoLabel";
			AddChild(infoLabel7);

			// Define and add Label 8
			infoLabel8 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "kannst du diese verändern",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 - 20)
			};
			infoLabel8.Alpha = 0.0f;
			infoLabel8.Name = "infoLabel";
			infoLabel8.ZPosition = 100;
			AddChild(infoLabel8);

			// Define and add Label 9
			infoLabel9 = new SKLabelNode("AppleSDGothicNeo-Bold")
			{
				Text = "TEATIME",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 60)
			};
			infoLabel9.Alpha = 0.0f;
			infoLabel9.Name = "infoLabel";
			infoLabel9.ZPosition = 100;
			AddChild(infoLabel9);

			// Do Actions on the Label Nodes (wait, fade in, wait, fade out)
			DoActionSequenceOnNode(infoLabel1, 1, 1, 2, 1, 0.8f);
			DoActionSequenceOnNode(infoLabel2, 2, 1, 2, 1, 0.8f);
			DoActionSequenceOnNode(infoLabel3, 6, 1, 2, 1, 0.8f);
			DoActionSequenceOnNode(infoLabel4, 7, 1, 2, 1, 0.8f);
			DoActionSequenceOnNode(infoLabel5, 11, 1, 2, 1, 0.8f);
			DoActionSequenceOnNode(infoLabel6, 12, 1, 2, 1, 0.8f);
			DoActionSequenceOnNode(infoLabel7, 16, 1, 2, 1, 0.7f);
			DoActionSequenceOnNode(infoLabel8, 17, 1, 2, 1, 0.7f);
			DoActionSequenceOnNode(infoLabel9, 0, 1, 20, 1, 0.7f);

			// Define and add Info Box rect
			infoNode = new SKSpriteNode();
			infoNode.Size = new CGSize(Frame.Width, Frame.Height);
			infoNode.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2);
			infoNode.Color = UIColor.FromHSB(0, 0, 0.3f);
			infoNode.ZPosition = 99;
			infoNode.Name = "infoNode";
			infoNode.Alpha = 0.0f;
			AddChild(infoNode);

			// Define and add Info Box rect 2
			infoNode2 = new SKSpriteNode();
			infoNode2.Size = new CGSize(Frame.Width - 60, 150);
			infoNode2.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 15);
			infoNode2.Color = UIColor.FromHSB(0, 0, 0.3f);
			infoNode2.ZPosition = 98;
			infoNode2.Name = "infoNode";
			infoNode2.Alpha = 0.0f;
			AddChild(infoNode2);

			// Define and add Info Box rect 3
			infoNode3 = new SKSpriteNode();
			infoNode3.Size = new CGSize(Frame.Width - 40, 150);
			infoNode3.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 15);
			infoNode3.Color = UIColor.FromHSB(0, 0, 0.3f);
			infoNode3.ZPosition = 98;
			infoNode3.Name = "infoNode";
			infoNode3.Alpha = 0.0f;
			AddChild(infoNode3);

			// Define and add Cancel Icon for the Info Box
			cancelSpark = new SKSpriteNode("sparkx");
			cancelSpark.Name = "cancelSpark";
			cancelSpark.ZPosition = 1000;
			cancelSpark.Alpha = 0.0f;
			cancelSpark.SetScale(0.6f);
			cancelSpark.Color = UIColor.FromHSB(0, 0, 0.0f);
			cancelSpark.Position = new CGPoint((View.Frame.Width - (40)), (View.Frame.Height / 2 + (70)));
			AddChild(cancelSpark);

			// Define and add Teatime Icon for the Info Box
			teatimeSprite = new SKSpriteNode("logoteatime_white_2_128@2x.png");
			teatimeSprite.Name = "infoNode";
			teatimeSprite.ZPosition = 1000;
			teatimeSprite.Alpha = 0.0f;
			teatimeSprite.SetScale(0.35f);
			teatimeSprite.Color = UIColor.FromHSB(0, 0, 0.0f);
			teatimeSprite.Position = new CGPoint((0 + (40)), (View.Frame.Height / 2 + (70)));
			AddChild(teatimeSprite);

			// Actions and Sequence for Info Nodes
			DoActionSequenceOnNode(infoNode, 0, 1, 20, 1, 0.3f);
			DoActionSequenceOnNode(infoNode3, 0, 1, 20, 1, 0.3f);
			DoActionSequenceOnNode(cancelSpark, 0, 1, 20, 1, 0.3f);
			DoActionSequenceOnNode(teatimeSprite, 0, 1, 20, 1, 0.3f);
		}

		// Do SKActions in a sequence on the given Sprite Node (wait, fade in, wait, fade out)
		private void DoActionSequenceOnNode(SKNode node, double waitSec, double fadeInSec, double waitSecMiddle, double fadeOutSec, nfloat maxAlpha)
		{
			SKAction sequence = SKAction.Sequence(SKAction.WaitForDuration(waitSec), SKAction.FadeAlphaTo(maxAlpha, fadeInSec), SKAction.WaitForDuration(waitSecMiddle), SKAction.FadeOutWithDuration(fadeOutSec));
			node.RunAction(sequence);
		}

		// Remove the Info Text (fade out)
		private void ReleaseInfoText()
		{
			SKAction actionOut = SKAction.FadeOutWithDuration(0.2);

			infoLabel1.RemoveAllActions();
			infoLabel2.RemoveAllActions();
			infoLabel3.RemoveAllActions();
			infoLabel4.RemoveAllActions();
			infoLabel5.RemoveAllActions();
			infoLabel6.RemoveAllActions();
			infoLabel7.RemoveAllActions();
			infoLabel8.RemoveAllActions();
			infoLabel9.RemoveAllActions();
			infoNode.RemoveAllActions();
			infoNode2.RemoveAllActions();
			infoNode3.RemoveAllActions();
			cancelSpark.RemoveAllActions();
			teatimeSprite.RemoveAllActions();

			infoLabel1.RunAction(actionOut);
			infoLabel2.RunAction(actionOut);
			infoLabel3.RunAction(actionOut);
			infoLabel4.RunAction(actionOut);
			infoLabel5.RunAction(actionOut);
			infoLabel6.RunAction(actionOut);
			infoLabel7.RunAction(actionOut);
			infoLabel8.RunAction(actionOut);
			infoLabel9.RunAction(actionOut);
			infoNode.RunAction(actionOut);
			infoNode2.RunAction(actionOut);
			infoNode3.RunAction(actionOut);
			cancelSpark.RunAction(actionOut);
			teatimeSprite.RunAction(actionOut);
		}

		private void CreateEmitterNode()
		{
			// Setup a intial Location
			var location = new CGPoint();
			location.X = (((View.Frame.Width / 2)));
			location.Y = (((View.Frame.Height / 4)));

			// Define paricles and initial settings
			particleEmitterNode = new SKEmitterNode();
			particleEmitterNode.Position = location;
			particleEmitterNode.NumParticlesToEmit = 0;
			particleEmitterNode.ZPosition = 2;
			particleEmitterNode.ParticleAlpha = 0.4f;
			particleEmitterNode.XAcceleration = 0;
			particleEmitterNode.YAcceleration = 1;
			particleEmitterNode.EmissionAngle = 100f;
			particleEmitterNode.TargetNode = this;
			particleEmitterNode.ParticleScale = 0.4f;
			particleEmitterNode.ParticleSpeedRange = 100f;
			particleEmitterNode.ParticleScaleRange = 0.5f;
			particleEmitterNode.ParticleScaleSpeed = -0.1f;
			particleEmitterNode.ParticleBirthRate = 500;
			particleEmitterNode.ParticlePositionRange = new CGVector(120f, 120f);
			particleEmitterNode.ParticleLifetimeRange = 10f;
			particleEmitterNode.ParticleRotationRange = 10f;
			particleEmitterNode.EmissionAngleRange = 200f;
			particleEmitterNode.ParticleTexture = SKTexture.FromImageNamed(("spark"));
		}

		// Update the particles speed and birthrate related to the location of touch
		private void UpdateEmitter(nfloat coordX)
		{
			//particleEmitterNode.ParticleScale = 0.6f * ((1/Frame.Width)*coordX) +0.2f ;
			//particleEmitterNode.ParticleScaleRange = 0.3f*((1 / Frame.Width) * coordX*10) +0.2f;
			particleEmitterNode.ParticleSpeedRange = 10f + (coordX / 5);
			particleEmitterNode.ParticleScaleSpeed = -0.02f;
			particleEmitterNode.ParticleBirthRate = 20f + (coordX / 5);
		}

		// Update blur factor and dimension 3
		private void UpdateBlurNode(int blur)
		{
			Proto2Dim3 = blur;
			particleEmitterNode.ParticleScale = 0.6f * ((1 / Frame.Width) * blur * 20) + 0.2f;
			particleEmitterNode.ParticleScaleRange = 0.3f * ((1 / Frame.Width) * blur * 50 * 10) + 0.2f;
		}

		// Update dimensions 1 and 2
		public void SetDimensions(nfloat coordX, nfloat coordY)
		{
			nfloat checkX = coordX;
			nfloat checkY = coordY;

			// Calculate the different dimensions based on the location
			if (checkY >= 4 * (Frame.Height / 5))
			{
				Proto2Dim1 = 1;
			}
			else if (checkY < 4 * (Frame.Height / 5) && checkY >= 3 * (Frame.Height / 5))
			{
				Proto2Dim1 = 2;
			}
			else if (checkY < 3 * (Frame.Height / 5) && checkY >= 2 * (Frame.Height / 5))
			{
				Proto2Dim1 = 3;
			}
			else if (checkY < 2 * (Frame.Height / 5) && checkY >= 1 * (Frame.Height / 5))
			{
				Proto2Dim1 = 4;
			}
			else
			{
				Proto2Dim1 = 5;
			}

			if (checkX >= 2 * (Frame.Width / 3))
			{
				Proto2Dim2 = 3;
			}
			else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
			{
				Proto2Dim2 = 2;
			}
			else
			{
				Proto2Dim2 = 1;
			}
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			// Called when a touch begins
			foreach (var touch in touches)
			{
				var touchc = touches.AnyObject as UITouch;
				var locationc = touchc.LocationInNode(this);
				var nodeAtLocation = GetNodeAtPoint(locationc);
				infoTouch = false;

				// Release the info text if cancel icon is clicked
				if (nodeAtLocation.Name == "cancelSpark")
				{
					ReleaseInfoText();
				}
				else
				{
					// If the touched node is not on the Info Text 
					if (nodeAtLocation.Name != "infoNode" && nodeAtLocation.Name != "infoLabel")
					{
						// If Info Icon is clicked set the info background and invert the info icon
						if (nodeAtLocation.Name == "info")
						{
							infoTouch = true;
							if (switchInfo == false)
							{
								switchInfo = true;
								SKAction seqTexture = SKAction.SetTexture(SKTexture.FromImageNamed(("background_n")));
								container.RunAction((seqTexture));
								SKAction seqTextureInfo = SKAction.SetTexture(SKTexture.FromImageNamed(("inforeverse")));
								infoSprite.RunAction((seqTextureInfo));
							}
							else
							{
								switchInfo = false;
								SKAction seqTextureNormal = SKAction.SetTexture(SKTexture.FromImageNamed(("background")));
								container.RunAction((seqTextureNormal));
								SKAction seqTextureInfoNormal = SKAction.SetTexture(SKTexture.FromImageNamed(("info")));
								infoSprite.RunAction((seqTextureInfoNormal));
							}
						}

						// Otherwise update the particles
						else
						{
							// Check click
							nfloat checkX = touchc.LocationInNode(this).X;
							nfloat checkY = touchc.LocationInNode(this).Y;

							particleEmitterNode.Position = locationc;
							UpdateEmitter(checkX);

							// If it is the first touch add the particles to the scene
							if (firstTouch == false)
							{
								AddChild(particleEmitterNode);
								firstTouch = true;
							}
						}
					}
				}
			}
		}

		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			var touch = touches.AnyObject as UITouch;
			var locationc = touch.LocationInNode(this);

			// Get the node at current touch location
			var nodeAtLocation = GetNodeAtPoint(locationc);

			// Release the info text if cancel icon is clicked
			if (nodeAtLocation.Name == "cancelSpark")
			{
				ReleaseInfoText();
			}
			else
			{
				// If the touched node is not on the Info Text or Icon update the Particles with location and set the updated values
				if (touch != null && infoTouch == false && nodeAtLocation.Name != "infoNode" && nodeAtLocation.Name != "infoLabel")
				{
					var offsetX = touch.LocationInNode(this).X;
					var offsetY = touch.LocationInNode(this).Y;

					// Updates the Particles
					UpdateEmitter(offsetX);
					particleEmitterNode.Position = locationc;
				}
			}
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			var touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				// Check the location of touch and update the dimensions
				var checkX = touch.LocationInView(View).X;
				var checkY = touch.LocationInView(View).Y;
				SetDimensions(checkX, checkY);
			}
		}

		private void PressHandler(UILongPressGestureRecognizer gestureRecognizer)
		{
			var image = gestureRecognizer.View;
			if (gestureRecognizer.State == UIGestureRecognizerState.Began)
			{
				var locationTouched = gestureRecognizer.LocationInView(View);
				var nodeAtLocation = GetNodeAtPoint(locationTouched);

				// If no Info Text is visible update the nodes with a blur factor
				if (nodeAtLocation.Name != "infoNode" && nodeAtLocation.Name != "infoLabel")
				{
					nfloat coordX = locationTouched.X;
					nfloat coordY = locationTouched.Y;
					SetDimensions(coordX, coordY);
					if (blurFactor == 1)
					{
						blurFactor = 5;
						UpdateBlurNode(blurFactor);
					}
					else {
						blurFactor = 1;
						UpdateBlurNode(blurFactor);
					}
					// Add vibration that user recognizes that the nodes are changing 
					SystemSound.Vibrate.PlaySystemSound();
				}
			}
		}

		public override void Update(double currentTime)
		{
			// Called before each frame is rendered
		}

		// Save Dimensions
		public void SaveProto2Input()
		{
			TeatimeItem item;
			item = new TeatimeItem();
			if (DatabaseMgmt.inputName != null)
			{
				item.Username = DatabaseMgmt.inputName;
			}
			else
			{
				item.Username = "Anonym";
			}
			item.dateInserted = DateTime.Now.ToLocalTime();
			item.Dim1 = Proto2Dim1;
			item.Dim2 = Proto2Dim2;
			item.Dim3 = Proto2Dim3;
			item.PrototypeNr = 2;
			item.Comment = "test";
			DatabaseMgmt.Database.SaveItem(item);
		}
	}
}
