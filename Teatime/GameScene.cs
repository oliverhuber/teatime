using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;

namespace Teatime
{
	public class GameScene : SKScene
	{
		// Prototype Dimension Mapping 
		public int Proto1Dim1 { get; set; }
		public int Proto1Dim2 { get; set; }
		public int Proto1Dim3 { get; set; }

		// Define Labels for the Intro Text
		SKLabelNode infoLabel1;
		SKLabelNode infoLabel2;
		SKLabelNode infoLabel3;
		SKLabelNode infoLabel4;
		SKLabelNode infoLabel5;
		SKLabelNode infoLabel6;
		SKLabelNode infoLabel7;
		SKLabelNode mySave;

		SKSpriteNode container;
		SKSpriteNode backgroundSprite;
		SKSpriteNode infoSprite;
		SKSpriteNode navSprite;
		SKSpriteNode infoNode;
		SKSpriteNode infoNode2;
		SKSpriteNode infoNode3;
		SKSpriteNode cancelSpark;
		SKSpriteNode teatimeSprite;

		CGPoint globalCenter;
		SKFieldNode fieldNode;

		bool switchInfo;
		bool infoTouch;

		int changeColor;

		protected GameScene(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void DidMoveToView(SKView view)
		{
			// Setup Scene with SKNodes and call the sparks generator
			Proto1Dim1 = 0;
			Proto1Dim2 = 0;
			Proto1Dim3 = 0;

			switchInfo = false;
			infoTouch = false;

			// Set inital bgcolor
			this.BackgroundColor = UIColor.FromRGBA(100, 200, 150, 120);

			// Background Sprite
			backgroundSprite = new SKSpriteNode("spark");
			backgroundSprite.ScaleTo(this.Frame.Size);
			backgroundSprite.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2);
			backgroundSprite.ZPosition = 0;
			backgroundSprite.Alpha = 0.3f;
			AddChild(backgroundSprite);

			// Add Gravity Field
			fieldNode = SKFieldNode.CreateSpringField();
			fieldNode.Enabled = false;
			fieldNode.Position = new CGPoint(Frame.Size.Width / 2, Frame.Size.Height / 2);
			fieldNode.Strength = 0.5f;
			fieldNode.Region = new SKRegion(Frame.Size);
			AddChild(fieldNode);

			// Change color, child not added only in test env
			mySave = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "next >",
				FontSize = 18,
				Position = new CGPoint(Frame.Width - 65, (Frame.Height - 53))
			};
			mySave.Name = "next";
			mySave.FontColor = UIColor.FromHSB((nfloat)0, 0, 3f);
			mySave.Alpha = 0.9f;
			mySave.ZPosition = 10;
			// this.AddChild(mySave);

			// Invisible sprite node to change to Info Mode, on the right top side infront of the info sprite 
			navSprite = new SKSpriteNode();
			navSprite.Name = "navSprite";
			navSprite.Alpha = 0.0000001f;
			navSprite.ZPosition = 10;
			navSprite.Color = UIColor.FromHSB((nfloat)0, 1, 0.0f);
			navSprite.Size = new CGSize(140, 70);
			navSprite.Position = new CGPoint((this.View.Frame.Width - (70)), (this.View.Frame.Height - (35)));
			AddChild(navSprite);

			// Container field to fill different background (Info Mode)
			container = new SKSpriteNode("background_info_p1");
			container.Size = new CGSize(Frame.Width, Frame.Height);
			container.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2);
			container.ZPosition = 0;
			container.Alpha = 0f;
			AddChild(container);

			// Sprite Node with the info icon
			infoSprite = new SKSpriteNode("info");
			infoSprite.Position = new CGPoint(Frame.Width - 40, Frame.Height - 40);
			infoSprite.ZPosition = 10;
			infoSprite.XScale = 0.6f;
			infoSprite.YScale = 0.6f;
			infoSprite.Alpha = 0.8f;
			infoSprite.Name = "info";
			AddChild(infoSprite);

			// Start with the info text
			setInfoText();

			// Set Color Flag
			changeColor = 0;

			// Generate Sparks
			generateSparks();
		}

		public void setInfoText()
		{
			// Define and add Label 1
			infoLabel1 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "Hier kannst du gleich deine Emotion",
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
				Text = "erfassen indem du den Bildschirm berührst",
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
				Text = "navigiere die Kreise mit dem Finger",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 10),
			};
			infoLabel3.Alpha = 0.0f;
			infoLabel3.ZPosition = 100;
			infoLabel3.Name = "infoLabel";
			AddChild(infoLabel3);

			// Define and add Label 4
			infoLabel4 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "die Farbe und Bewegung verändert sich",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 - 20),
			};
			infoLabel4.Alpha = 0.0f;
			infoLabel4.ZPosition = 100;
			infoLabel4.Name = "infoLabel";
			AddChild(infoLabel4);

			// Define and add Label 5
			infoLabel5 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "wenn Farbe und Emotion übereinstimmen",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 10),
			};
			infoLabel5.Alpha = 0.0f;
			infoLabel5.ZPosition = 100;
			infoLabel5.Name = "infoLabel";
			AddChild(infoLabel5);

			// Define and add Label 6
			infoLabel6 = new SKLabelNode("AppleSDGothicNeo-UltraLight")
			{
				Text = "hebe den Finger ab dem Bildschirm",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 - 20),
			};
			infoLabel6.Alpha = 0.0f;
			infoLabel6.Name = "infoLabel";
			infoLabel6.ZPosition = 100;
			AddChild(infoLabel6);

			// Define and add Label 7
			infoLabel7 = new SKLabelNode("AppleSDGothicNeo-Bold")
			{
				Text = "TEATIME",
				FontSize = 18,
				Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 60),
			};
			infoLabel7.Alpha = 0.0f;
			infoLabel7.Name = "infoLabel";
			infoLabel7.ZPosition = 100;
			AddChild(infoLabel7);

			// Do Actions on the Label Nodes (wait, fade in, wait, fade out)
			doActionSequenceOnNode(infoLabel1, 1, 1, 2, 1, 0.8f);
			doActionSequenceOnNode(infoLabel2, 2, 1, 2, 1, 0.8f);
			doActionSequenceOnNode(infoLabel3, 6, 1, 2, 1, 0.8f);
			doActionSequenceOnNode(infoLabel4, 7, 1, 2, 1, 0.8f);
			doActionSequenceOnNode(infoLabel5, 11, 1, 2, 1, 0.8f);
			doActionSequenceOnNode(infoLabel6, 12, 1, 2, 1, 0.8f);
			doActionSequenceOnNode(infoLabel7, 0, 1, 16, 1, 0.7f);

			// Define and add Info Box rect
			infoNode = new SKSpriteNode();
			infoNode.Size = new CGSize(Frame.Width, Frame.Height);
			infoNode.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2);
			infoNode.Color = UIColor.FromHSB((nfloat)0, 0, 0.3f);
			infoNode.ZPosition = 99;
			infoNode.Name = "infoNode";
			infoNode.Alpha = 0.0f;
			AddChild(infoNode);

			// Define and add Info Box rect 2
			infoNode2 = new SKSpriteNode();
			infoNode2.Size = new CGSize(Frame.Width - 60, 150);
			infoNode2.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 15);
			infoNode2.Color = UIColor.FromHSB((nfloat)0, 0, 0.3f);
			infoNode2.ZPosition = 98;
			infoNode2.Name = "infoNode";
			infoNode2.Alpha = 0.0f;
			AddChild(infoNode2);

			// Define and add Info Box rect 3
			infoNode3 = new SKSpriteNode();
			infoNode3.Size = new CGSize(Frame.Width - 40, 150);
			infoNode3.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2 + 15);
			infoNode3.Color = UIColor.FromHSB((nfloat)0, 0, 0.3f);
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
			cancelSpark.Color = UIColor.FromHSB((nfloat)0, 0, 0.0f);
			cancelSpark.Position = new CGPoint((this.View.Frame.Width - (40)), (this.View.Frame.Height / 2 + (70)));
			AddChild(cancelSpark);

			// Define and add Teatime Icon for the Info Box
			teatimeSprite = new SKSpriteNode("logoteatime_white_2_128@2x.png");
			teatimeSprite.Name = "infoNode";
			teatimeSprite.ZPosition = 1000;
			teatimeSprite.Alpha = 0.0f;
			teatimeSprite.SetScale(0.35f);
			teatimeSprite.Color = UIColor.FromHSB((nfloat)0, 0, 0.0f);
			teatimeSprite.Position = new CGPoint((0 + (40)), (this.View.Frame.Height / 2 + (70)));
			AddChild(teatimeSprite);

			// Actions and Sequence for Info Nodes
			doActionSequenceOnNode(infoNode, 0, 1, 16, 1, 0.3f);
			doActionSequenceOnNode(infoNode3, 0, 1, 16, 1, 0.3f);
			doActionSequenceOnNode(cancelSpark, 0, 1, 16, 1, 0.3f);
			doActionSequenceOnNode(teatimeSprite, 0, 1, 16, 1, 0.3f);
		}

		// Do SKActions in a sequence on the given Sprite Node (wait, fade in, wait, fade out)
		public void doActionSequenceOnNode(SKNode node, double waitSec, double fadeInSec, double waitSecMiddle, double fadeOutSec, nfloat maxAlpha)
		{
			SKAction sequence = SKAction.Sequence(SKAction.WaitForDuration(waitSec), SKAction.FadeAlphaTo(maxAlpha, fadeInSec), SKAction.WaitForDuration(waitSecMiddle), SKAction.FadeOutWithDuration(fadeOutSec));
			node.RunAction(sequence);
		}

		// Remove the Info Text (fade out)
		public void releaseInfoText()
		{
			SKAction actionOut = SKAction.FadeOutWithDuration(0.2);

			infoLabel1.RemoveAllActions();
			infoLabel2.RemoveAllActions();
			infoLabel3.RemoveAllActions();
			infoLabel4.RemoveAllActions();
			infoLabel5.RemoveAllActions();
			infoLabel6.RemoveAllActions();
			infoLabel7.RemoveAllActions();
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
			infoNode.RunAction(actionOut);
			infoNode2.RunAction(actionOut);
			infoNode3.RunAction(actionOut);
			cancelSpark.RunAction(actionOut);
			teatimeSprite.RunAction(actionOut);
		}

		// Update Center, calculate new Alpha Value and Mass for All Spark and related Parent Nodes depending on the location of touch
		public void updateCenter(nfloat offsetX, nfloat offsetY)
		{
			CGPoint center = globalCenter;

			// Change Texture for all SkarkNodes
			foreach (var spark in Children.OfType<SparkNode>())
			{
				// Set the globalCenter value to the nodes
				spark.centerOfNode = center;
				spark.parentNode.centerOfNode = center;

				// Remove all Actions
				spark.RemoveAllActions();
				spark.parentNode.RemoveAllActions();

				// Set the new Alpha Value
				spark.Alpha = ((1 / (Frame.Width) * (offsetX)) / 10f * 4f) + 0.1f;
				spark.parentNode.Alpha = spark.Alpha;

				// Depending on the location of touch set the MassOfBody (Whole view is divided in 5 different parts)
				if ((offsetY < 5 * (Frame.Height / 5) && offsetY >= 4 * (Frame.Height / 5)))
				{
					spark.massOfBody(1);
					spark.parentNode.massOfBody(1);
				}
				else if ((offsetY < 4 * (Frame.Height / 5) && offsetY >= 3 * (Frame.Height / 5)))
				{
					spark.massOfBody(0.8f);
					spark.parentNode.massOfBody(0.8f);

				}
				else if ((offsetY < 3 * (Frame.Height / 5) && offsetY >= 2 * (Frame.Height / 5)))
				{
					spark.massOfBody(0.6f);
					spark.parentNode.massOfBody(0.6f);
				}
				else if ((offsetY < 2 * (Frame.Height / 5) && offsetY >= 1 * (Frame.Height / 5)))
				{
					spark.massOfBody(0.4f);
					spark.parentNode.massOfBody(0.4f);
				}
				else if ((offsetY < 1 * (Frame.Height / 5) && offsetY >= 0 * (Frame.Height / 5)))
				{
					spark.massOfBody(0.2f);
					spark.parentNode.massOfBody(0.2f);

				}
			}
		}

		// Revert the center value back to the node itselfs center value
		public void revertCenter()
		{
			// Remove the global Center and set the fixed center of the nodes itself
			foreach (var spark in Children.OfType<SparkNode>())
			{
				spark.centerOfNode = spark.centerOfNodeFixed;
				spark.parentNode.centerOfNode = spark.parentNode.centerOfNodeFixed;
			}
		}

		// Update all SparkNodes with speed, random factor, disturbfactor and vibration
		public void updateSparks(double speed, bool random, bool disturb, int disturbFactor, bool vibrate)
		{
			// Update all SparkNodes with speed, random factor, disturbfactor and vibration
			foreach (var spark in Children.OfType<SparkNode>())
			{
				spark.moveAnimation(speed, random, disturb, disturbFactor, vibrate);
			}
		}

		// Generate the sparks particles
		public void generateSparks()
		{
			// Inital speed 
			var speed = 0;

			// Generate 3 nodes per 6 rows
			for (int i = 1; i <= 3; i++)
			{
				for (int y = 1; y <= 6; y++)
				{
					// Calculate location of each Spark (5 Sparks per row, for 10 rows)
					var location = new CGPoint();
					location.X = (((this.View.Frame.Width / 6) * (2 * i)) - (this.View.Frame.Width / 6));
					location.Y = (((this.View.Frame.Height / 12) * (2 * y)) - (this.View.Frame.Height / 12));

					// Define inital sparks with location and alpha
					var sprite = new SparkNode("spark")
					{
						Position = location,
						XScale = 1.6f,
						YScale = 1.6f,
						Alpha = 0.3f
					};

					// Set the Center for the node
					sprite.centerOfNode = location;
					sprite.centerOfNodeFixed = location;

					// Define Rotation, is zero because of the inital speed
					var action = SKAction.RotateByAngle(NMath.PI * speed * i, 10.0);
					sprite.RunAction(SKAction.RepeatActionForever(action));

					// Position in comparsion to other SpriteNodes
					sprite.ZPosition = 1;

					// Define ParentNode, each SparkNode gets a ParentNode
					var parent = new ParentNode("spark")
					{
						Position = location,
						XScale = 0.9f,
						YScale = 0.9f,
						Alpha = 0.3f

					};

					// Set the Center for the node
					parent.centerOfNode = location;
					parent.centerOfNodeFixed = location;

					// Define Rotation, is zero because of the inital speed
					var paction = SKAction.RotateByAngle(NMath.PI * speed * i, 10.0);
					parent.RunAction(SKAction.RepeatActionForever(paction));

					// Position in comparsion to other SpriteNodes
					parent.ZPosition = 1;

					// Add the SparkNode to the scene
					AddChild(sprite);

					// Add the ParentNode to the SparkNode
					sprite.parentNode = parent;

					// Add the ParentNode to the scene
					AddChild(parent);
				}
			}
			// Do first update of the all sparks, that they will have a base movement
			updateSparks(1, true, false, 0, false);
		}

		// Calculate the different dimensions based on the location
		// Update all Spark Nodes with local Animation and speed
		public void setDimensions(nfloat coordX, nfloat coordY)
		{
			double speed;
			nfloat checkX = coordX;
			nfloat checkY = coordY;
			if (checkY >= 4 * (Frame.Height / 5))
			{
				speed = 4;
				Proto1Dim1 = 1;
				updateSparks(speed, false, false, 0, false);
			}
			else if (checkY < 4 * (Frame.Height / 5) && checkY >= 3 * (Frame.Height / 5))
			{
				speed = 3.5;
				Proto1Dim1 = 2;
				updateSparks(speed, true, true, 2, false);
			}
			else if (checkY < 3 * (Frame.Height / 5) && checkY >= 2 * (Frame.Height / 5))
			{
				speed = 3;
				Proto1Dim1 = 3;
				updateSparks(speed, true, true, 4, false);
			}
			else if (checkY < 2 * (Frame.Height / 5) && checkY >= 1 * (Frame.Height / 5))
			{
				speed = 2.5;
				Proto1Dim1 = 4;
				updateSparks(speed, true, true, 4, true);
			}
			else {
				speed = 2;
				Proto1Dim1 = 5;
				updateSparks(speed, true, true, 6, true);

			}

			if (checkX >= 2 * (Frame.Width / 3))
			{
				Proto1Dim2 = 3;
				Proto1Dim3 = 3;
			}
			else if (checkX < 2 * (Frame.Width / 3) && checkX >= 1 * (Frame.Width / 3))
			{
				Proto1Dim2 = 2;
				Proto1Dim3 = 2;
			}
			else
			{
				Proto1Dim2 = 1;
				Proto1Dim3 = 1;
			}
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			// Called when a touch begins
			foreach (var touch in touches)
			{
				UITouch touchc = touches.AnyObject as UITouch;

				// Check click
				var locationc = ((UITouch)touchc).LocationInNode(this);
				var checkX = ((UITouch)touchc).LocationInView(View).X;
				var checkY = ((UITouch)touchc).LocationInView(View).Y;

				// other coordinate system
				//var checkX = ((UITouch)touchc).LocationInNode(this).X;
				//var checkY = ((UITouch)touchc).LocationInNode(this).Y;

				// Get Sprite Node at current location
				var nodeAtLocation = GetNodeAtPoint(locationc);
				if (nodeAtLocation.Name == "cancelSpark")
				{
					releaseInfoText();
				}
				else 
				{
					if (nodeAtLocation.Name != "infoNode" && nodeAtLocation.Name != "infoLabel")
					{
						// If the info button is clicked change background
						if (nodeAtLocation.Name == "info" || nodeAtLocation.Name == "navSprite")
						{
							infoTouch = true;

							// Activate information background
							if (switchInfo == false)
							{
								//setInfoText();
								container.Alpha = 1f;
								switchInfo = true;

								SKAction seqTextureInfo = SKAction.SetTexture(SKTexture.FromImageNamed(("inforeverse")));
								infoSprite.RunAction((seqTextureInfo));
							}

							// Deactivate information background
							else 
							{
								container.Alpha = 0f;
								switchInfo = false;

								SKAction seqTextureInfoNormal = SKAction.SetTexture(SKTexture.FromImageNamed(("info")));
								infoSprite.RunAction((seqTextureInfoNormal));
							}
						}

						// if next is clicked, change background color mode
						else if ((nodeAtLocation is SKLabelNode && nodeAtLocation.Name == "next") || nodeAtLocation.Name == "navSprite")
						{
							changeColor++;
							if (changeColor % 3 == 0)
							{
								// Background Calculating (Default Mode)
								this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));
							}
							else if (changeColor % 3 == 1)
							{
								// Background Calculating
								this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 1f, (nfloat)(((checkX / Frame.Width) / 2) * 1 + ((0.3333333f))));
							}
							else if (changeColor % 3 == 2)
							{
								// Background Calculating
								this.BackgroundColor = UIColor.FromHSB((nfloat)0, 0, (nfloat)(((checkX / Frame.Width) / 2) * 1 + ((0.3333333f))));
							}
							else {
								// Background Calculating
								this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));
							}
						}
					}
				}
			}
		}

		// If finger moved on screen
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			UITouch touch = touches.AnyObject as UITouch;
			var locationc = ((UITouch)touch).LocationInNode(this);

			// get the node at touched location
			var nodeAtLocation = GetNodeAtPoint(locationc);

			// Release the info text if cancel icon is clicked
			if (nodeAtLocation.Name == "cancelSpark")
			{
				releaseInfoText();
			}
			else 
			{
				// If the info icon is not clicked and the info text box is not visible get the location of touch and update the background color
				if (touch != null && infoTouch == false && nodeAtLocation.Name != "infoNode" && nodeAtLocation.Name != "infoLabel")
				{
					// Enable the Physics field
					fieldNode.Enabled = true;

					// Get the latest X and Y coordinates
					// 0 of X i on top
					float offsetX = (float)(touch.LocationInView(View).X);
					float offsetY = (float)(touch.LocationInView(View).Y);

					// other coordinate system
					// 0 of Y is bottom
					var checkX_Location = ((UITouch)touch).LocationInNode(this).X;
					var checkY_Location = ((UITouch)touch).LocationInNode(this).Y;

					float checkX = offsetX;
					float checkY = offsetY;

					fieldNode.Position = new CGPoint(checkX_Location, checkY_Location);

					if (changeColor % 3 == 0)
					{
						var manipulate = (checkY_Location / Frame.Height) + 0.18;
						if (manipulate > 1)
						{
							manipulate = manipulate - 1;
						}

						// Background Calculating
						this.BackgroundColor = UIColor.FromHSB((nfloat)manipulate, 0.5f, (nfloat)0.8f);//(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));

					}
					else if (changeColor % 3 == 1)
					{
						// Background Calculating
						this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 1f, (nfloat)(((checkX / Frame.Width) / 2) * 1 + ((0.3333333f))));
					}
					else if (changeColor % 3 == 2)
					{
						// Background Calculating
						this.BackgroundColor = UIColor.FromHSB((nfloat)0, 0, (nfloat)(((checkX / Frame.Width) / 2) * 1 + ((0.3333333f))));
					}
					else {
						// Background Calculating
						this.BackgroundColor = UIColor.FromHSB((nfloat)(checkY / Frame.Height), 0.5f, (nfloat)(((checkX / Frame.Width) / 3) * 2 + ((0.3333333f))));
					}

					// Check to which part the finger is moved to and update the sparks
					globalCenter = new CGPoint(checkX_Location, checkY_Location);
					setDimensions(offsetX, offsetY);
					this.updateCenter(offsetX, offsetY);
				}
			}
		}
		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			UITouch touch = touches.AnyObject as UITouch;
			var locationc = ((UITouch)touch).LocationInNode(this);
			var nodeAtLocation = GetNodeAtPoint(locationc);
			if (touch != null)
			{
				// If Info Text Box is not visible, revert center for the spark nodes
				if (nodeAtLocation.Name != "infoNode" && nodeAtLocation.Name != "infoLabel")
				{
					//Release the Changed center
					this.revertCenter();
					fieldNode.Enabled = false;
					infoTouch = false;

					// Check touch location
					var checkX = ((UITouch)touch).LocationInView(View).X;
					var checkY = ((UITouch)touch).LocationInView(View).Y;

					// Update dimensions
					this.setDimensions(checkX, checkY);
				}
			}
		}

		// Save Dimensions
		public void saveProto1Input()
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
			item.Dim1 = Proto1Dim1;
			item.Dim2 = Proto1Dim2;
			item.Dim3 = Proto1Dim3;
			item.PrototypeNr = 1;
			item.Comment = "test";
			DatabaseMgmt.Database.SaveItem(item);
		}
	}
}
