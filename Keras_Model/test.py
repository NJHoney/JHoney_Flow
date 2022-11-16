import keras

def Initialize():
	base_Model = keras.applications.ResNet50(weights='imagenet', input_shape=(224,224,3), include_top=False)
	base_Model.summary()
	return "good"

