https://towardsdatascience.com/getting-started-with-tensorflow-js-d74c7085fc0b
https://blog.logrocket.com/how-to-set-up-node-typescript-express/
https://medium.com/riow/install-tensorflow-on-mac-a42526b96e72
https://www.tensorflow.org/js/tutorials/conversion/import_keras
if you get error of tensorflow not found or probuff related issue then follow steps from this link
https://iq.opengenus.org/tensorflow-not-found-pip/


python3.11 -m venv .

pip3 install --upgrade https://storage.googleapis.com/tensorflow/mac/cpu/tensorflow-2.11.0-cp310-cp310-macosx_10_14_x86_64.whl

or 
download whl from the url and install manually

pip3 install --upgrade tensorflow-2.11.0-cp310-cp310-macosx_10_14_x86_64.whl

Lets verify tensorlfow
run python3
and import tensorflow as tf
You can get message
if it like below
`This TensorFlow binary is optimized with oneAPI Deep Neural Network Library (oneDNN) to use the following CPU instructions in performance-critical operations:  AVX2 FMA
To enable them in other operations, rebuild TensorFlow with the appropriate compiler flags.`

then it means:

`An important part of Tensorflow is that it is supposed to be fast. With a suitable installation, it works with CPUs, GPUs, or TPUs. Part of going fast means that it uses different code depending on your hardware. Some CPUs support operations that other CPUs do not, such as vectorized addition (adding multiple variables at once). Tensorflow is simply telling you that the version you have installed can use the AVX and AVX2 operations and is set to do so by default in certain situations (say inside a forward or back-prop matrix multiply), which can speed things up. This is not an error, it is just telling you that it can and will take advantage of your CPU to get that extra speed out.

Note: AVX stands for Advanced Vector Extensions.`

pip3 install -U scikit-learn
pip3 install -U matplotlib


