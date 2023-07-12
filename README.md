# CSharp-Python-TCP-Image-Transmission

This repository provides a demonstration of establishing a TCP connection between C# and Python and transmitting a camera image between the two.

## Prerequisites

- Unity (for the C# script)
- Python 3
- OpenCV (Python package)
- NumPy

## Usage

1. Clone the repository
2. Run the Python receiver script
3. Open the Unity project in Unity Editor.
4. Attach the `CameraSenderTest` script to a camera in the scene. (Make sure to have multiple cameras, the camera with the script attached to it will not display anything)
5. Update the `pythonServerIP` and `pythonServerPort` variables in the `CameraSenderTest` script to match the IP address and port of the Python receiver.
6. Play the Unity scene.
7. The camera image will be transmitted to the Python receiver script, which will display the received image using OpenCV.

