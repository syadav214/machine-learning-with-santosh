import pandas as pd
import matplotlib.pyplot as plt

def get_max_close(symbol):
    df = pd.read_csv("data/{}.csv".format(symbol))
    # keep your csv column a number
    df[df.columns[2]].plot()
    # if 2 columns
    #df[[df.columns[2],df.columns[7]]].plot()
    plt.show()
    return df[df.columns[7]].max()
    

def test_run():
    for symbol in ['GRASIM','ASIANPAINT']:
        print("max close")
        print(symbol, get_max_close(symbol))
    
if __name__ == "__main__":
    test_run()