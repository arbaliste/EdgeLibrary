﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EdgeLibrary;
using Microsoft.Xna.Framework;

namespace EdgeDemo.CheckersGame
{
    public class HotseatMenu : MenuBase
    {
        public HotseatMenu() : base("HotseatMenu")
        {
            TextSprite title = new TextSprite(Config.MenuTitleFont, "Hotseat Game", new Vector2(EdgeGame.WindowSize.X / 2, EdgeGame.WindowSize.Y * 0.05f)) { Color = Config.MenuTextColor };
            Components.Add(title);

            TextSprite subTitle = new TextSprite(Config.MenuSubtitleFont, "Click!", new Vector2(EdgeGame.WindowSize.X / 2, EdgeGame.WindowSize.Y * 0.1f)) { Color = Config.MenuTextColor };
            Components.Add(subTitle);

            Button startButton = new Button("grey_button00", new Microsoft.Xna.Framework.Vector2(EdgeGame.WindowSize.X / 2, EdgeGame.WindowSize.Y * 0.7f)) { ClickTexture = EdgeGame.GetTexture("grey_button01"), MouseOverTexture = EdgeGame.GetTexture("grey_button02"), Color = Config.MenuButtonColor, Scale = new Vector2(1) };
            startButton.SetColors(Config.MenuButtonColor);
            startButton.OnRelease += (x, y) => { Config.ThisGameType = Config.GameType.Hotseat; BoardManager.ResetGame = true; MenuManager.SwitchMenu("GameMenu"); };
            Components.Add(startButton);

            TextSprite startButtonText = new TextSprite(Config.MenuButtonTextFont, "Start Game", startButton.Position);
            Components.Add(startButtonText);

            Input.OnKeyRelease += Input_OnKeyRelease;
        }

        void Input_OnKeyRelease(Microsoft.Xna.Framework.Input.Keys key)
        {
            if (MenuManager.SelectedMenu == this && key == Config.BackKey)
            {
                MenuManager.SwitchMenu(MenuManager.PreviousMenu.Name);
            }
        }
    }
}
