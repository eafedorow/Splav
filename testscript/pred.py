import sys
import pandas as pd
import pickle as pc
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn.preprocessing import LabelEncoder as lb
import xgboost
from sklearn import metrics


def create_model(db_path: str):
    df = pd.read_excel(db_path)
    # sizes = df['плена 0/1+'].value_counts(sort=1)
    # print(sizes)
    # plt.pie(sizes,autopct='%1.1f%%')
    df.drop(['Признак зачистки, признак'], axis=1, inplace=True)
    df.drop(['плена1'], axis=1, inplace=True)
    df.drop(['плена сумма'], axis=1, inplace=True)
    # print(df.head())

    df = df.dropna()

    # dft = pd.read_excel("Копия массив 2022.xlsx")
    # # sizes = df['плена 0/1+'].value_counts(sort=1)
    # # print(sizes)
    # # plt.pie(sizes,autopct='%1.1f%%')
    # dft.drop(['Определение переходных с плавки на плавку слябов(до перехода), нет/да [0/1]'], axis=1, inplace=True)
    # dft.drop(['Определение переходных с плавки на плавку слябов(после перехода), нет/да [0/1]'], axis=1, inplace=True)
    # dft.drop(['Признак зачистки, признак'], axis=1, inplace=True)
    # dft.drop(['Изменение скорости разливки на расстоянии 1-го метра сляба более, чем на 0,1 м/мин, нет/да [0/1]'], axis=1,
    #          inplace=True)
    # dft.drop(['Скорость разливки на сляб максим, м/с'], axis=1, inplace=True)
    # dft.drop(['плена1'], axis=1, inplace=True)
    # dft.drop(['плена сумма'], axis=1, inplace=True)
    # # print(df.head())
    #
    # dft = dft.dropna()

    Y = df["плена"]
    Y = Y.astype('double')
    # Y_test = dft["плена"]
    # Y_test = Y_test.astype('double')
    X = df.drop(labels=["плена"], axis=1)
    # x_test = dft.drop(labels=["плена"], axis=1)
    # X_train, X_test, y_train, y_test = train_test_split(X, Y, test_size=0.3, random_state=20)
    model = xgboost.XGBClassifier(eta=0.01,
                                  max_depth=6,
                                  n_estimator=100,
                                  min_child_weight=6,
                                  reg_lamda=1,
                                  reg_alpha=0,
                                  sumsample=0.5,
                                  colsample_bytree=0.5)

    le = lb()
    Y = le.fit_transform(Y)
    model.fit(X, Y)
    y_pred_train = model.predict(X)
    # print(metrics.classification_report(Y, y_pred_train))
    # y_pred_test = model.predict(X)
    # print(metrics.classification_report(Y, y_pred_test))
    conf_mat = metrics.confusion_matrix(Y, y_pred_train)
    conf_mat = pd.DataFrame(conf_mat, index=model.classes_, columns=model.classes_)
    # print(conf_mat)
    y_feature_imp = df.columns[:-1]
    x_feature_imp = model.feature_importances_
    # dat = pd.DataFrame({'real': Y, 'pred': y_pred_train})
    # dat.to_excel('result_train.xlsx')
    # sns.set_theme()
    # sns.scatterplot(data=dat, x='real', y='pred')
    # plt.show()
    # y_pred_test = model.predict(x_test)
    # dat_test = pd.DataFrame({'real': Y_test, 'pred': y_pred_test})
    # dat_test.to_excel('result_test.xlsx')
    # print(metrics.classification_report(Y_test, y_pred_test))
    # conf_mat = metrics.confusion_matrix(Y_test, y_pred_test)
    # conf_mat = pd.DataFrame(conf_mat, index=model.classes_, columns=model.classes_)
    # print(conf_mat)

    # Обрезать путь бд и сюда
    split_path = db_path.split('\\')
    split_path[-1] = 'model.txt'
    model_path = ''
    for ind in split_path:
        model_path += '\\' + ind
    model_path = model_path[1:]

    with open(model_path, "wb") as f:
        pc.dump(model, f)

    # sns.barplot(x=x_feature_imp, y=y_feature_imp)
    # plt.xlabel('Важность признаков')
    # plt.ylabel('Признаки')
    # plt.title('Визуализация важных признаков')
    # plt.show()


if __name__ == '__main__':
    create_model(r'C:\Users\navip\OneDrive\Документы\GitHub\Splav\testscript\train_data.xlsx')  # train_data.xlsx sys.argv[1]
