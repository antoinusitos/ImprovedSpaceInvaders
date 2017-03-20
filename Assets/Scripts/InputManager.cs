using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class InputManager: MonoBehaviour
{
    public float triggerDeadZone = 0.5f;

    private static InputManager _instance;

    public static InputManager GetInstance()
    {
        return _instance;
    }

    private bool playerIndexSet = false;
    private PlayerIndex playerIndex;
    private GamePadState state;
    private GamePadState prevState;
    
    void Awake ()
    {
        _instance = this;
    }
	
	void Update ()
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
    }

    public float GetStickPosX()
    {
        return state.ThumbSticks.Left.X;
    }

    public float GetStickPosY()
    {
        return state.ThumbSticks.Left.Y;
    }

    public bool RightTriggerPressed()
    {
        return state.Triggers.Right >= triggerDeadZone ? true : false;
    }

    public bool SuperPowerButtonPressed()
    {
        if(prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public bool AButtonPressed()
    {
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }
}
