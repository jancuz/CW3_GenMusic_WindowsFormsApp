import pandas as pd
import numpy as np
from sklearn.preprocessing import StandardScaler
from sklearn.model_selection import train_test_split

from keras.models import model_from_json

jfile = open("D:/3 курс/курсовая/CW3_GenMusic_WindowsFormsApp/CW3_GenMusic_WindowsFormsApp//Neuro/model.json", "r")
loaded_model = jfile.read()
jfile.close()
loaded_model = model_from_json(loaded_model)
loaded_model.compile(loss='sparse_categorical_crossentropy', optimizer='adam', metrics=['accuracy'])

data = pd.read_csv('D:/3 курс/курсовая/CW3_GenMusic_WindowsFormsApp/CW3_GenMusic_WindowsFormsApp/bin/Debug/Data/data'
                   '.csv')

scaler = StandardScaler()
X = scaler.fit_transform(np.array(data.iloc[:, 1:-1], dtype = float))
data['class_predicted'] = np.NaN
predictions = loaded_model.predict(X)
for i in range(0, predictions.shape[0]):
    data.at[i, 'class_predicted'] = np.argmax(predictions[i])

data.to_csv('D:/3 курс/курсовая/CW3_GenMusic_WindowsFormsApp/CW3_GenMusic_WindowsFormsApp/bin/Debug/Data/data.csv',
            index=False)
