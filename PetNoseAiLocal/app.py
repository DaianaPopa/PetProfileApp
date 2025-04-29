from flask import Flask, request, jsonify
import face_recognition
import os
from PIL import UnidentifiedImageError

app = Flask(__name__)


PET_FOLDER = "pets"


def load_known_faces():
    known_encodings = []
    known_names = []

    for filename in os.listdir(PET_FOLDER):
        if filename.lower().endswith((".jpg", ".png")):
            try:
                image_path = os.path.join(PET_FOLDER, filename)
                image = face_recognition.load_image_file(image_path)
                encodings = face_recognition.face_encodings(image)
                if encodings:
                    known_encodings.append(encodings[0])
                    
                    known_names.append(os.path.splitext(filename)[0])
            except Exception as e:
                print(f"Failed to process {filename}: {e}")
    
    return known_encodings, known_names


known_encodings, known_names = load_known_faces()

@app.route("/identify", methods=["POST"])
def identify():
    if 'image' not in request.files:
        return jsonify({"error": "No file part"}), 400

    file = request.files['image']
    if file.filename == '':
        return jsonify({"error": "No selected file"}), 400

    try:
        unknown = face_recognition.load_image_file(file)
        unknown_encodings = face_recognition.face_encodings(unknown)

        if not unknown_encodings:
            return jsonify({"match": False, "message": "No face/nose found"}), 200

        results = face_recognition.compare_faces(known_encodings, unknown_encodings[0])

        if True in results:
            matched_idx = results.index(True)
            matched_name = known_names[matched_idx]  
            return jsonify({"match": True, "pet": matched_name}), 200
        else:
            return jsonify({"match": False}), 200

    except UnidentifiedImageError:
        return jsonify({"error": "Cannot identify image file"}), 400

@app.route("/reload", methods=["POST"])
def reload_faces():
    global known_encodings, known_names
    known_encodings, known_names = load_known_faces()
    return jsonify({"status": "Reloaded", "count": len(known_names)})

if __name__ == "__main__":
    app.run(port=5000, debug=True)
