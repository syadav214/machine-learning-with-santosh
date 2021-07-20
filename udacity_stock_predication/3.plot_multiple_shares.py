import pandas as pd
import matplotlib.pyplot as plt

def test_run():
    start_date = '2020-12-01'
    end_date = '2021-07-19'
    dates = pd.date_range(start_date, end_date)
    
    df = pd.DataFrame(index=dates)
    for symbol in ['GRASIM','ASIANPAINT']:
        dfTemp = pd.read_csv("data/{}.csv".format(symbol), index_col="Date", parse_dates=True, usecols=["Date","CLOSE"], na_values=["nan"])
        dfTemp = dfTemp.rename(columns={"CLOSE": symbol})
        df=df.join(dfTemp)
    
    #drop na values
    df = df.dropna()
    
    #normalize the dataframe using first row
    df = df/df.iloc[0,:]
    
    ax = df.plot(title="stock", fontsize=12)
    ax.set_xlabel("date")
    ax.set_ylabel("price")
    plt.show()
    
if __name__ == "__main__":
    test_run()