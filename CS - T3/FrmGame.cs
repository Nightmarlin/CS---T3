using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS___T3 {
    public partial class FrmGame : Form {

        public FrmGame() {
            InitializeComponent();
            Panels_LastPanel = this.PnlOverlay;
            Panels_CurrentPanel = this.PnlOverlay;
            this.PnlOverlay.Show();
            Game_Buttons = new List<Button>();
            Game_Buttons.Add(btn_0_0);
            Game_Buttons.Add(btn_0_1);
            Game_Buttons.Add(btn_0_2);
            Game_Buttons.Add(btn_1_0);
            Game_Buttons.Add(btn_1_1);
            Game_Buttons.Add(btn_1_2);
            Game_Buttons.Add(btn_2_0);
            Game_Buttons.Add(btn_2_1);
            Game_Buttons.Add(btn_2_2);
        }

        #region Whole Form Code
        private void FrmGame_OnKeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Escape:
                    if (Panels_CurrentPanel == PnlOverlay) {
                        Panels_GoToLastPanel(PnlOverlay);
                    } else {
                        Panels_GoToPanel(Panels_CurrentPanel, this.PnlOverlay);
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region PnlOverlay Code
        private void BtnStart_Click(object sender, EventArgs e) {
            if (Game_Playing == false && BtnGoStopContinue.Text == "Start Game") {
                Game_Playing = true; 
                BtnGoStopContinue.Text = "Continue Setup";
                Panels_GoToPanel(this.PnlOverlay, this.PnlGameSelect);
            } else if (BtnGoStopContinue.Text == "Exit Game") {
                EndGame(2);
            } else {
                Panels_GoToLastPanel(this.PnlOverlay);
            }
        }

        private void BtnCurrentStats_Click(object sender, EventArgs e) {
            Panels_GoToPanel(this.PnlOverlay, this.PnlGameStats);
        }

        private void BtnBackToGame_Click(object sender, EventArgs e) {
            if (Game_Playing) { Panels_GoToPanel(this.PnlOverlay, this.PnlGame); };
        }

        #endregion

        #region PnlGameSelect Code
        public Color GameSelect_ChosenP1Colour;
        public Color GameSelect_ChosenP2Colour;

        private void CbxPvP_Click(object sender, EventArgs e) {
            if (CbxPvP.Checked == false) {
                TxtP2Name.Enabled = false;
                TxtP2Name.Text = "Computer";
                CbxP1First.Checked = true;
                CbxP1First.Enabled = false;
                BtnPlay.Text = "Player" + Environment.NewLine + "V" + Environment.NewLine + "Computer";
            } else {
                TxtP2Name.Enabled = true;
                TxtP2Name.Text = "";
                CbxP1First.Enabled = true;
                BtnPlay.Text = "Player" + Environment.NewLine + "V" + Environment.NewLine + "Player";
            }
        }

        private void BtnPlay_Click(object sender, EventArgs e) {
            if (TxtP1Name.Text == "" || TxtP2Name.Text == "") {
                MessageBox.Show("Please enter both names");
            } else if (CheckColours() == false) {
                MessageBox.Show("Please select different colours");
            } else {
                if (CbxPvP.Checked) {
                    BeginGame();
                } else {
                    MessageBox.Show("Play vs Computer is not ready yet. Sorry for that.");
                }
            }
        }

        private bool CheckColours() {
            if  (RdbP1Black.Checked == true & RdbP2Black.Checked == true ) { return false; } else {
                if ( RdbP1Blue.Checked == true & RdbP2Blue.Checked == true ) { return false; } else {
                    if ( RdbP1Red.Checked == true & RdbP2Red.Checked == true ) { return false; } else {
                        if ( RdbP1Green.Checked == true & RdbP2Green.Checked == true ) { return false; } else {
                            if ( RdbP1Gray.Checked == true & RdbP2Gray.Checked == true ) { return false; } else {
                                if ( RdbP1Yellow.Checked == true & RdbP2Yellow.Checked == true ) { return false; } else {
                                    if ( RdbP1Cyan.Checked == true & RdbP2Cyan.Checked == true ) { return false; } else {
                                        if ( RdbP1Magenta.Checked == true & RdbP2Magenta.Checked == true ) { return false; } else {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ColoursChanged(object sender, EventArgs e) {
            if (RdbP1Black.Checked) { GameSelect_ChosenP1Colour = Color.Black; } else {
                if ( RdbP1Blue.Checked ) { GameSelect_ChosenP1Colour = Color.Blue; } else {
                    if ( RdbP1Red.Checked ) { GameSelect_ChosenP1Colour = Color.Red; } else {
                        if ( RdbP1Green.Checked ) { GameSelect_ChosenP1Colour = Color.Green; } else {
                            if ( RdbP1Gray.Checked ) { GameSelect_ChosenP1Colour = Color.Gray; } else {
                                if ( RdbP1Yellow.Checked ) { GameSelect_ChosenP1Colour = Color.Yellow; } else {
                                    if ( RdbP1Cyan.Checked ) { GameSelect_ChosenP1Colour = Color.Cyan; } else {
                                        GameSelect_ChosenP1Colour = Color.Magenta;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if ( RdbP2Black.Checked ) { GameSelect_ChosenP2Colour = Color.Black; } else {
                if ( RdbP2Blue.Checked ) { GameSelect_ChosenP2Colour = Color.Blue; } else {
                    if ( RdbP2Red.Checked ) { GameSelect_ChosenP2Colour = Color.Red; } else {
                        if ( RdbP2Green.Checked ) { GameSelect_ChosenP2Colour = Color.Green; } else {
                            if ( RdbP2Gray.Checked ) { GameSelect_ChosenP2Colour = Color.Gray; } else {
                                if ( RdbP2Yellow.Checked ) { GameSelect_ChosenP2Colour = Color.Yellow; } else {
                                    if ( RdbP2Cyan.Checked ) { GameSelect_ChosenP2Colour = Color.Cyan; } else {
                                        GameSelect_ChosenP2Colour = Color.Magenta;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            UpdateScreenColours();

            //MessageBox.Show("P1: " + GameSelect_ChosenP1Colour.ToString() + Environment.NewLine +
            //    "P2: " + GameSelect_ChosenP2Colour.ToString());
        }
        #endregion

        #region PnlGame Code
        private void BeginGame() {
            BtnGoStopContinue.Text = "Exit Game";
            Panels_GoToPanel(PnlGameSelect, PnlGame);
            Game_CurrentPlayerTurn = CbxP1First.Checked;
            Game_Board = new int[3, 3]; // C# takes array initailzers by length, but uses them by address
            Game_Playing = true;
            TurnCount = 0;
            foreach (Button button in Game_Buttons) { button.Text = ""; };
            BtnBackToGame.Enabled = true;
            BtnCurrentStats.Enabled = true;
            Stats_LblP1Name.Text = "P1: " + TxtP1Name.Text;
            Stats_LblP2Name.Text = "P2: " + TxtP2Name.Text;
            if (CbxP1First.Checked) {
                Stats_LblWhoGoesFirst.Text = TxtP1Name.Text + " goes first";
                Stats_LblCurrentTurn.Text = "It's " + TxtP1Name.Text + "'s turn";
            } else {
                Stats_LblWhoGoesFirst.Text = TxtP2Name.Text + " goes first";
                Stats_LblCurrentTurn.Text = "It's " + TxtP2Name.Text + "'s turn";
            }
            UpdateScreenColours();
        }
        private void EndGame(int ByWin) {
            BtnGoStopContinue.Text = "Start Game";
            BtnCurrentStats.Enabled = false;
            BtnBackToGame.Enabled = false;
            Game_Playing = false;
            TurnCount = 0;
            foreach (Button button in Game_Buttons) { button.Text = ""; };
            Game_Board = new int[3, 3];
            if (ByWin == 1) {
                Panels_GoToPanel(Panels_CurrentPanel, PnlOverlay);
            } else if (ByWin == 2) {
                MessageBox.Show("You Resigned", "COWARDICE I SAY!!", MessageBoxButtons.OK);
                Panels_GoToPanel(Panels_CurrentPanel, PnlOverlay);
            } else {
                MessageBox.Show("It's a tie", "This is how T3 should be played", MessageBoxButtons.OK);
                Panels_GoToPanel(Panels_CurrentPanel, PnlOverlay);
            }
        }

        private void GameButton_Click(object sender, EventArgs e) {
            GetTileStats();
            if (sender.GetType() == typeof(Button) && Game_Playing == true) {
                Button Clicked = (Button)sender;
                String[] BtnName = Clicked.Name.Split('_');
                if (Game_CurrentPlayerTurn == true && Clicked.Text == "") {
                    Clicked.Text = "1";
                    Game_Board[int.Parse(BtnName[1]), int.Parse(BtnName[2])] = 1;
                    Stats_LblCurrentTurn.Text = "It's " + TxtP2Name.Text + "'s turn";
                } else if (Game_CurrentPlayerTurn == false && Clicked.Text == "") {
                    Clicked.Text = "9";
                    Game_Board[int.Parse(BtnName[1]), int.Parse(BtnName[2])] = 9;
                    Stats_LblCurrentTurn.Text = "It's " + TxtP1Name.Text + "'s turn";
                }
                UpdateScreenColours();
                TurnCount += 1;
                WinChecker();
                Game_CurrentPlayerTurn = !Game_CurrentPlayerTurn;
                if (TurnCount == 9) { EndGame(3); };
            }
            GetTileStats();
        }
        #endregion

        #region General Panel Code

        Panel Panels_LastPanel;
        Panel Panels_CurrentPanel;

        private void Panels_GoToLastPanel(Panel sender) {
            sender.Hide();
            Panels_LastPanel.Show();
            Panels_CurrentPanel = Panels_LastPanel;
            if ( sender != PnlOverlay ) { Panels_LastPanel = sender; }
        }

        private void Panels_GoToPanel(Panel sender, Panel Target) {
            sender.Hide();
            Target.Show();
            Panels_CurrentPanel = Target;
            if ( sender != PnlOverlay ) { Panels_LastPanel = sender; }
        }
        #endregion

        #region Game
        bool Game_Playing = false;
        bool Game_CurrentPlayerTurn = true;
        int[,] Game_Board;
        int TurnCount = 0;

        List<Button> Game_Buttons;

        private void UpdateScreenColours() {
            /*
            for (int XLooper = 0; XLooper <= 2; XLooper++) {
                for (int YLooper = 0; YLooper <= 2; YLooper++) {
                    MessageBox.Show("." + XLooper.ToString() + ".");
                    Control[] ActiveBtn = PnlGame.Controls.Find("\"btn_" + XLooper.ToString() + "_" + YLooper.ToString() + "\"", true);
                    ActiveBtn[0].Text = Game_Board[XLooper, YLooper].ToString();
                }
            }
            */
            foreach (Button button in Game_Buttons) {
                if (button.Text == "") {
                    button.BackColor = Color.FromKnownColor(KnownColor.Control);
                    button.ForeColor = button.BackColor;
                } else if (button.Text == "1") {
                    button.BackColor = GameSelect_ChosenP1Colour;
                    button.ForeColor = button.BackColor; //Color.FromArgb(button.BackColor.ToArgb() ^ 0xffffff);
                } else if (button.Text == "9") {
                    button.BackColor = GameSelect_ChosenP2Colour;
                    button.ForeColor = button.BackColor; //Color.FromArgb(button.BackColor.ToArgb() ^ 0xffffff);
                }
            }
            GetTileStats();
            Stats_BtnP1Colour.BackColor = GameSelect_ChosenP1Colour;
            Stats_BtnP2Colour.BackColor = GameSelect_ChosenP2Colour;
        }

        private void GetTileStats() {
            int P1Count = 0;
            int P2Count = 0;

            foreach (Button button in Game_Buttons) {
                if (button.Text == "1") {
                    P1Count += 1;
                } else if (button.Text == "9") {
                    P2Count += 1;
                }
            }

            Stats_P1TileCount.Text = P1Count.ToString();
            Stats_P2TileCount.Text = P2Count.ToString();
        }

        private void WinChecker() {
            int Victory = 0;
            for (int XLooper = 0; XLooper <= 2; XLooper++) {
                int total = Game_Board[XLooper, 0] + Game_Board[XLooper, 1] + Game_Board[XLooper, 2];
                if (total == 3) {
                    Victory = 1;
                    break;
                } else if (total == 27) {
                    Victory = 2;
                    break;
                }
            }

            for (int YLooper = 0; YLooper <= 2; YLooper++) {
                int total = Game_Board[0, YLooper] + Game_Board[1, YLooper] + Game_Board[2, YLooper];
                if (total == 3) {
                    Victory = 1;
                    break;
                } else if (total == 27) {
                    Victory = 2;
                    break;
                }
            }

            int DiagTLBR = Game_Board[0, 0] + Game_Board[1, 1] + Game_Board[2, 2];
            int DiagBLTR = Game_Board[0, 2] + Game_Board[1, 1] + Game_Board[2, 0];

            if (DiagBLTR == 3 || DiagBLTR == 3 ) {
                Victory = 1;
            } else if (DiagTLBR == 27 || DiagTLBR == 27) {
                Victory = 2;
            }


            if (Victory == 1) {
                MessageBox.Show(TxtP1Name.Text + " Wins!", "Victory", MessageBoxButtons.OK);
                EndGame(1);
            } else if (Victory == 2) {
                MessageBox.Show(TxtP2Name.Text + " Wins!", "Victory", MessageBoxButtons.OK);
                EndGame(1);
            }

        }

        #endregion

    }
}
