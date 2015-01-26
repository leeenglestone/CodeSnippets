file_ = open('Test.txt', 'w')
file_.write('whatever')
file_.close()

#Alternatively you can use this version, which will automatically close the file, even if there was an exception:
with open('Test.txt', 'w') as file_:
    file_.write('whatever')