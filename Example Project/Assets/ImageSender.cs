using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

public class ImageSender : MonoBehaviour
{
    private const string pythonServerIP = "127.0.0.1";
    private const int pythonServerPort = 1234;

    private TcpClient client;
    private NetworkStream stream;
    private Texture2D texture;
    private RenderTexture renderTexture;

    private const int maxSize = 60000;
    private const float transmissionInterval = 0.5f; // Time interval between data transmission in seconds
    private float timeElapsed = 0f;

    private void Start()
    {
        client = new TcpClient(pythonServerIP, pythonServerPort);
        stream = client.GetStream();

        // Create a RenderTexture and assign it to the camera's targetTexture
        renderTexture = new RenderTexture(256, 256, 10);
        GetComponent<Camera>().targetTexture = renderTexture;
        texture = new Texture2D(renderTexture.width, renderTexture.height);
    }

    private async void Update()
    {
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= transmissionInterval)
        {
            //SendCameraData(texture);
            await SendCameraData(texture);
            timeElapsed = 0f;
        }
    }

    private void SendDataToPython(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    private async Task SendCameraData(Texture2D texture)
    {
        byte[] bytes = texture.EncodeToPNG();
        print("Transmitted data size: " + bytes.Length);
        if (bytes.Length > maxSize)
        {
            print("Image size exceeds the limit. Skipping transmission.");
            return;
        }
        await stream.WriteAsync(bytes, 0, bytes.Length);

        
    }
}
