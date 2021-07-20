import pandas as pd

def test_run():
    start_date = '2020-12-01'
    end_date = '2021-07-19'
    dates = pd.date_range(start_date, end_date)
    
    df1 = pd.DataFrame(index=dates)
    
    dfGrasim = pd.read_csv("data/GRASIM.csv", index_col="Date", parse_dates=True, usecols=["Date","CLOSE"], na_values=["nan"])
    
    df1 = df1.join(dfGrasim)
    df1 = df1.dropna()
    print(df1)
    
if __name__ == "__main__":
    test_run()