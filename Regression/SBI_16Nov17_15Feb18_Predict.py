# -*- coding: utf-8 -*-
"""
Created on Fri Feb 16 14:44:33 2018

@author: santosh.yadav
"""

# Importing the libraries
import numpy as np
import matplotlib.pyplot as plt
import pandas as pd

# Importing the dataset
dataset = pd.read_csv('PNB_01DecNov17_28Feb18_Stockdata.csv')
X = dataset.iloc[:, 0:1].values
y = dataset.iloc[:, 1:2].values

from sklearn.svm import SVR
svr_lin = SVR(kernel = 'linear', C= 1e3)
#svr_poly = SVR(kernel ='poly', C=1e3, degree=2)
svr_rbf = SVR(kernel='rbf',C=1e3,gamma=0.1)


svr_lin.fit(X, y)
#svr_poly.fit(X, y) #this function takes time
svr_rbf.fit(X, y)

Y_predict_lin = svr_lin.predict(X)
#Y_predict_poly = svr_poly.predict(X)
Y_predict_rbf = svr_rbf.predict(X)


plt.scatter(X, y, color = 'black', label='Data')
plt.plot(X,Y_predict_lin,color='green',label ='Model I')
#plt.plot(X,Y_predict_poly,color='blue',label ='Polynomial Model')
plt.plot(X,Y_predict_rbf,color='red',label ='Model II')
plt.xlabel('Days')
plt.ylabel('Price')
plt.title('PNB')
plt.legend()
plt.show()

svr_rbf.predict(62)












