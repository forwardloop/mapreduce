A CSV file contains values of a parameter read off cars' engines during a race. The data comes on channels with 1 microsecond frequency. Each row contains the name of a driver, parameter value and a timestamp. 

This programme computes an average value of the parameter for every driver and prints top three positions.

Given the input:

```
Alonzo,4.32,1479136979429120
Verstrappen,4.75,1479136979429120
Alonzo,4.88,1479136979429121
```

the output should be:

```
Verstrappen,4.75
Alonzo,4.60
```
