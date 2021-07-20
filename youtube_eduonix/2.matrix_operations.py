import sys
import numpy as np

A = np.matrix([[1, 2], [3, 4]])
B = np.ones((2, 2), dtype=np.int)
print(A)
print(B)

# Addition
C = A + B
print(C)

# Subtraction
C = A - B
print(C)

# Multiplication
C = A * B
print(C)

# Transpose - Way 1
A = np.array(range(9))
A = A.reshape(3, 3)
print(A)
B = A.T
print(B)

# Transpose - Way 2
A = np.matrix([[1, 2, 4], [2, 3, 6], [4, 7, 9]])
print(A)
B = A.T
print(B)

# Transpose - Way 3
A = np.array(range(10))
A = A.reshape(2, 5)
B = A.T
print(B)
print(A.shape)
print(B.shape)


# Tensor
A = np.ones((3, 3, 3, 3, 3, 3, 3, 3, 3, 3))
print(A.shape)
print(len(A.shape))
print(A.size)
