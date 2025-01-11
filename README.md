
# Screen and Audio Streaming Server

This project creates a **screen and audio streaming server** using **C#** that streams the system's screen and audio in real-time to connected devices over a local network. The server captures the screen using `System.Drawing` and audio with the `NAudio` library, and clients can access the stream via HTTP.

## Features
- **Real-Time Screen Streaming**: Streams the primary system screen in real-time.
- **Audio Streaming**: Streams live audio captured from the system microphone.
- **Multipart Streaming**: Utilizes `multipart/x-mixed-replace` to stream both screen and audio data simultaneously.
- **Network Accessibility**: Accessible by devices on the same local network.

## Requirements
- **.NET Framework 4.7.2+**
- **NAudio** (for audio capturing)

## Setup

1. **Clone the repository** or download the project files.
2. **Install dependencies**:
    - Open the project in Visual Studio.
    - Restore NuGet packages.
3. **Run the application**:
    - Build and run the project in Visual Studio.
    - The server will start listening on the specified IP and port (`http://localhost:8080` by default).

## Usage

- **Start the server**: Click on the “Start” button to begin capturing and streaming your screen and audio.
- **Access the stream**: Open a web browser on any device within the same Wi-Fi network and go to the provided URL (e.g., `http://192.168.1.x:8080`).
- **Stop the server**: Click on the “Stop” button to halt the streaming server.

## Technologies
- **C#** and **Windows Forms** for the application UI.
- **NAudio** for audio capturing.
- **System.Drawing** for screen capture.
- **HttpListener** for serving the streaming data via HTTP.

## Troubleshooting
- Ensure that your device and the streaming server are on the same Wi-Fi network.
- Check firewall settings to allow incoming connections on the server port.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
