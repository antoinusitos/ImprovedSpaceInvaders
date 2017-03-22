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
    private PlayerIndex player2Index;
    private GamePadState state;
    private GamePadState statep2;
    private GamePadState prevState;
    private GamePadState prevStatep2;

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
        state = GamePad.GetState(testPlayerIndex);

        PlayerIndex testPlayer2Index = (PlayerIndex)1;
        GamePadState testState2 = GamePad.GetState(testPlayer2Index);
        if (testState2.IsConnected)
        {
            player2Index = testPlayer2Index;
            playerIndexSet = true;
        }

        prevStatep2 = state;
        statep2 = GamePad.GetState(testPlayer2Index);
    }

    public void Vibration(int index)
    {
        GamePad.SetVibration((PlayerIndex)index, 1.0f, 1.0f);
    }

    public float GetStickPosX()
    {
        return state.ThumbSticks.Left.X;
    }

    public float GetStickPosY()
    {
        return state.ThumbSticks.Left.Y;
    }

    public float GetStickPosXP2()
    {
        return statep2.ThumbSticks.Left.X;
    }

    public float GetStickPosYP2()
    {
        return statep2.ThumbSticks.Left.Y;
    }

    public bool RightTriggerPressed()
    {
        return state.Triggers.Right >= triggerDeadZone ? true : false;
    }

    public bool RightTriggerPressedP2()
    {
        return statep2.Triggers.Right >= triggerDeadZone ? true : false;
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

    public bool SuperPowerButtonPressedP2()
    {
        if (prevStatep2.Buttons.B == ButtonState.Released && statep2.Buttons.B == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public bool AButtonPressedP2()
    {
        if (prevStatep2.Buttons.A == ButtonState.Released && statep2.Buttons.A == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }
}
