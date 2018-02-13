using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Oscillator : MonoBehaviour {

    public double frequency = 440d;
    private double increment;
    private double phase;
    private double sampling_frequency = 48000d;

    public float gain;
    [Range(0f, 1f)]
    public float volume = 0.1f;

    public float[] frequencies;
    public KeyCode[] keys;
    public int thisFrequency;

    private void Start()
    {
        //Set the frequencies to the A major scale
        frequencies = new float[8];
        frequencies[0] = 440; //A
        frequencies[1] = 494; //B
        frequencies[2] = 554; //C#
        frequencies[3] = 587; //D
        frequencies[4] = 659; //E
        frequencies[5] = 740; //F#
        frequencies[6] = 831; //G#
        frequencies[7] = 880; //A

        keys = new KeyCode[8];
        keys[0] = KeyCode.Keypad1;
        keys[1] = KeyCode.Keypad2;
        keys[2] = KeyCode.Keypad3;
        keys[3] = KeyCode.Keypad4;
        keys[4] = KeyCode.Keypad5;
        keys[5] = KeyCode.Keypad6;
        keys[6] = KeyCode.Keypad7;
        keys[7] = KeyCode.Keypad8;
    }

    private void Update()
    {
        for (int i = 0; i < 8; i++)
        {
            if(Input.GetKeyDown(keys[i]))
            {
                gain = volume;
                frequency = frequencies[i];
            }
            if (Input.GetKeyUp(keys[i]))
            {
                gain = 0;
            }
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2 * Mathf.PI / sampling_frequency;

        for(int i = 0; i < data.Length; i += channels)
        {
            phase += increment;

            //Square waves boiii
            if(gain * Mathf.Sin((float)phase) >= 0 * gain)
            {
                data[i] = (float)gain * 0.6f;
            }
            else
            {
                data[i] = (float)-gain * 0.6f;
            }

            if(channels == 2)
            {
                data[i + 1] = data[i];
            }

            if(phase > Mathf.PI * 2)
            {
                phase = 0d;
            }
        }
    }
}
