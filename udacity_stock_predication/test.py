import pandas as pd

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
    
    print(df.mean())

    
if __name__ == "__main__":
    test_run()