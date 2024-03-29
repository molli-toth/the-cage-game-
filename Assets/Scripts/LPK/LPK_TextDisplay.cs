﻿/***************************************************
File:           LPK_TextDisplay.cs
Authors:        Christopher Onorati
Last Updated:   12/21/18
Last Version:   2.17

Description: 
  This component controls the appearance of a text display
  such as a health or cooldown bar. It can be hooked up to 
  LPK_Health or other components.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   /* Text */

/**
* CLASS NAME  : LPK_TextDisplay
* DESCRIPTION : This component can be added to any object with a TextMesh to act as a display of sorts.
**/
public class LPK_TextDisplay : LPK_LogicBase
{
    /************************************************************************************/

    public enum DisplayType
    {
        TIMER,
        COUNTER,
        COUNTER_OVER_TOTAL,
    }

    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Text to put before the display text.  EX: Timer.")]
    [Rename("Beggining Text")]
    public string m_sStartText;

    [Tooltip("What display mode to use for the text.")]
    [Rename("Display Mode")]
    public DisplayType m_eDisplayMode = DisplayType.TIMER;

    [Tooltip("Max number of digits to display for a timer.")]
    [Rename("Timer Max Decimals")]
    public uint m_iMaxDecimals = 3;

    /************************************************************************************/
    TextMesh m_cTextMesh;
    Text m_cText;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Initializes components and events.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        m_cTextMesh = GetComponent<TextMesh>();
        m_cText = GetComponent<Text>();

        LPK_EventManager.OnLPK_DisplayUpdate += OnEvent;
    }

    /**
    * FUNCTION NAME: OnEvent
    * DESCRIPTION  : Updates the display text of the mesh based on passed data.
    * INPUTS       : data - float value 1 = current time, float value 2 = end time
    * OUTPUTS      : None
    **/
    override protected void OnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Incorrect object.
        if (!ShouldRespondToEvent(data))
            return;

        if(data.m_flData.Count < 1)
            return;

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Display Update");

        if (m_cTextMesh != null)
            UpdateGameText(data);
        else if (m_cText != null)
            UpdateUIText(data);
    }

    /**
    * FUNCTION NAME: UpdateGameText
    * DESCRIPTION  : Updates the display text of the mesh based on passed data.
    * INPUTS       : data - float value 1 = current time, float value 2 = end time
    * OUTPUTS      : None
    **/
    void UpdateGameText(LPK_EventManager.LPK_EventData data)
    {
        if (m_eDisplayMode == DisplayType.COUNTER)
        {
            string displayText = m_sStartText + data.m_flData[0];
            m_cTextMesh.text = displayText;
        }
        else if (m_eDisplayMode == DisplayType.COUNTER_OVER_TOTAL)
        {
            if(data.m_flData.Count < 2)
                return;

            string displayText = m_sStartText + data.m_flData[0] + "/" + data.m_flData[1];
            m_cTextMesh.text = displayText;
        }
        else if (m_eDisplayMode == DisplayType.TIMER)
        {
            if (m_iMaxDecimals > 0)
            {
                if(data.m_flData.Count < 2)
                    return;

                string displayText = m_sStartText + string.Format("{0:N" + m_iMaxDecimals + "}", Mathf.Min(data.m_flData[0], data.m_flData[1]));
                m_cTextMesh.text = displayText;
            }
            else
            {
                if(data.m_flData.Count < 2)
                    return;

                string displayText = m_sStartText + ((int)Mathf.Min(data.m_flData[0], data.m_flData[1])).ToString();
                m_cTextMesh.text = displayText;
            }
        }
    }

    /**
    * FUNCTION NAME: UpdateUIText
    * DESCRIPTION  : Updates the display text of the mesh based on passed data.
    * INPUTS       : data - float value 1 = current time, float value 2 = end time
    * OUTPUTS      : None
    **/
    void UpdateUIText(LPK_EventManager.LPK_EventData data)
    {
        if (m_eDisplayMode == DisplayType.COUNTER)
        {
            string displayText = m_sStartText + data.m_flData[0];
            m_cText.text = displayText;
        }
        else if (m_eDisplayMode == DisplayType.COUNTER_OVER_TOTAL)
        {
            if(data.m_flData.Count < 2)
                return;

            string displayText = m_sStartText + data.m_flData[0] + "/" + data.m_flData[1];
            m_cText.text = displayText;
        }
        else if (m_eDisplayMode == DisplayType.TIMER)
        {
            if (m_iMaxDecimals > 0)
            {
                if(data.m_flData.Count < 2)
                    return;

                string displayText = m_sStartText + string.Format("{0:N" + m_iMaxDecimals + "}", Mathf.Min(data.m_flData[0], data.m_flData[1]));
                m_cText.text = displayText;
            }
            else
            {
                if(data.m_flData.Count < 2)
                    return;

                string displayText = m_sStartText + ((int)Mathf.Min(data.m_flData[0], data.m_flData[1])).ToString();
                m_cText.text = displayText;
            }
        }
    }
}
