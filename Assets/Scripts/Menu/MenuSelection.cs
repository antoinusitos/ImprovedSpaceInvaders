using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class MenuSelection : MonoBehaviour
{
    public Image newGame;
    public Image quit;

    private bool playerIndexSet = false;
    private PlayerIndex playerIndex;
    private GamePadState state;
    private GamePadState prevState;

    enum selected
    {
        newGame,
        quit,
    }

    private selected _currentSelected;

    private void Start()
    {
        _currentSelected = selected.newGame;
        ShowColor();
    }

    private void Update()
    {
        PlayerIndex testPlayerIndex = (PlayerIndex)0;
        GamePadState testState = GamePad.GetState(testPlayerIndex);
        if (testState.IsConnected)
        {
            playerIndex = testPlayerIndex;
            playerIndexSet = true;
        }

        prevState = state;
        state = GamePad.GetState(0);

        if(state.ThumbSticks.Left.Y < -0.2f)
        {
            if (_currentSelected == selected.newGame)
            {
                _currentSelected = selected.quit;
                ShowColor();
            }
        }
        else if (state.ThumbSticks.Left.Y > 0.2f)
        {
            if (_currentSelected == selected.quit)
            {
                _currentSelected = selected.newGame;
                ShowColor();
            }
        }

        AButtonPressed();
    }

    void ShowColor()
    {
        if (_currentSelected == selected.newGame)
        {
            newGame.color = Color.red;
            quit.color = Color.white;
        }
        else
        {
            newGame.color = Color.white;
            quit.color = Color.red;
        }
    }

    public void AButtonPressed()
    {
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            Execute();
        }
    }

    public void Execute()
    {
        if(_currentSelected == selected.quit)
        {
            Application.Quit();
        }
        else
        {
            Application.LoadLevel(1);
        }
    }

}
