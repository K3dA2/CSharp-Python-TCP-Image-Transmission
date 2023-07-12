import socket
import cv2
import numpy as np

def main():
    server_ip = "localhost"
    server_port = 1234

    # Create a socket object
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server_socket:
        # Bind the socket to the server IP and port
        server_socket.bind((server_ip, server_port))
        
        # Listen for incoming connections
        server_socket.listen(1)
        print("Python server is listening for connections...")

        # Accept a client connection
        conn, addr = server_socket.accept()
        print("Connection established with:", addr)

        # Receive and process data from the client
        with conn:
            while True:
                # Receive data from the client
                # Note: If the expected length of the received data exceeds 60000, update the value passed to conn.recv()
                data = conn.recv(60000)
                if not data:
                    # Break the loop if no more data is received
                    break

                # Decode the received data as an image
                print("Received data size:", len(data))
                img_data = np.frombuffer(data, dtype=np.uint8)
                print("Image data shape:", img_data.shape)
                img = cv2.imdecode(img_data, cv2.IMREAD_COLOR)

                # Display the image using OpenCV
                cv2.imshow("Received Image", img)
                cv2.waitKey(1)

if __name__ == "__main__":
    main()
