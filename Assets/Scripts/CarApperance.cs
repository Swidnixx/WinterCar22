using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarApperance : MonoBehaviour
{
    public int playerNumber;

    public string playerName;
    public Color carColor;
    public TextMeshProUGUI nameText;
    public Renderer carRenderer;

    public Camera backCamera;

    public void SetAvatar(string name, Color color)
    {
        playerName = name;
        carColor = color;

        nameText.text = playerName;
        carRenderer.material.color = carColor;
        nameText.color = carColor;
    }

    public void SetLocalPlayer()
    {
        FindObjectOfType<CameraController>().SetCamera(this.transform);
        playerName = PlayerPrefs.GetString("PlayerName");

        Color playerColor = MenuController.IntToColor(PlayerPrefs.GetInt("Red"), PlayerPrefs.GetInt("Green"), PlayerPrefs.GetInt("Blue"));
        carColor = playerColor;

        nameText.text = playerName;
        carRenderer.material.color = carColor;
        nameText.color = carColor;

        RenderTexture rt = new RenderTexture(600, 200, 0);
        backCamera.targetTexture = rt;
        FindObjectOfType<RaceController>().SetMirrorPic(backCamera);
    }
}
