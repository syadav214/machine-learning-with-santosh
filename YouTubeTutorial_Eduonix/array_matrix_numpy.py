# runnable in jupyter labs and spyder (anaconda)

import sys
import numpy as np

print("Python: {}", format(sys.version))
print("Numpy: {}", format(np.__version__)

# scalar == single value
x=6
print(x)

# vector == array
x=np.array((1, 2, 3))
print(x)

# matrix == 2d array
x=np.matrix([[1, 2, 3], [4, 5, 6], [7, 8, 9]])
print(x)
print('matrix dimenstions {}'.format(x.shape))
print('matrix size {}'.format(x.size))

x=np.zeros((5, 5))
print(x)

x=np.ones((3, 3))
print('matrix dimenstions {}'.format(x.shape))

# tensor == more than 2d array
x=np.ones((3, 3, 3))
print(x)
print('tensor dimenstions {}'.format(x.shape))

A=np.ones((5, 5), dtype=np.int)
A[0, 1]=2
print(A)

A[:, 1]=3  # works on all columns
print(A)

A=np.ones((5, 5, 5), dtype=np.int)
print(A)

A[:, 0, 0]=6
print(A)
