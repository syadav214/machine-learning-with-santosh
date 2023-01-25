from tensorflow.keras.models import load_model

new_model = load_model('saved_models/seq.h5')

new_model.summary()
