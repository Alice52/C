
/*/01.Ѱ������--------------------------------------------------------------------------------------------------------------
#include<iostream>
#include<cmath>
using namespace std;

int main() {
	int i, j, num = -1;
	for (i = 100; i <= 200; i++)
	{
		for (j = 2; j < i / 2; j++)
		{
			if (i%j == 0)
				break;
		}
		if (j >= i / 2)
		{
			num++;
			if (num % 5 == 0)
				cout << endl;
			cout << i << '\t';
		}
	}
	return 0;
}
*/
















/*
#include<iostream>
#include<iomanip>
using namespace std;
int main()
{
	int a[3][3] = { {1,2,3},{4,5,6},{7,8,9} }, i, j, *p;
	for (i = 0, p = a[0]; p <= a[2] + 2; p++, i++)
	{
		if (i&&i % 3 == 0)
			cout << '\n';
		cout << *p;
	}
	//cin.get();
	return 0;
}
*/


















/*//02.�۰��������=======================================================================================================================
#include <iostream>
using namespace std;

void insertSort(int a[], int n)
{
	for (int i = 1; i < n; ++i)
	{
		if (a[i] < a[i - 1])
		{
			int low = 0;
			int high = i - 1;
			int j;
			int temp = a[i];
			while (low <= high)
			{
				int mid = (low + high) / 2;
				if (a[mid] == a[i])
				{//��a[i]����a[mid]�ұ�
					for (j = i - 1; j > mid; --j)
					{
						a[j + 1] = a[j];
					}
					a[mid + 1] = temp;
				}
				else if (a[mid] < a[i])
				{
					low = mid + 1;
				}
				else
				{
					high = mid - 1;
				}
			}
			if (low > high)//˵����ǰ�����Ԫ�ز������ź����������,��ʱӦ�ý�Ԫ�ط���index����low��������index����high+1����
			{
				for (j = i - 1; j >= low; --j)
				{
					a[j + 1] = a[j];
				}
				a[low] = temp;
			}
		}
	}

}
int main()
{
	const int N = 7;
	int a[] = { 3,1,7,9,2,5,4 };
	insertSort(a, N);
	for (int i = 0; i < N; ++i)
	{
		cout << a[i] << " " << endl;
	}
	return 0;
}
*/



/*  //һ�����顢Array��Vector��valarray=================================================================

#include<iostream>
using namespace std;

int main()
{
	string city[10];

	city[0] = "����";
	city[1] = "�Ϻ�";
	city[2] = "�Ϸ�";
	city[3] = "�Ͼ�";
	city[4] = "����";
	city[5] = "����";

	int year[] = { 1984,2002,2008,2014 };
	cout << year[3];
	system("pause");
}
*/



/*  //02.vector
#include <iostream>
#include <vector>
using namespace std;

int main()
{
	int n;  //number of color array
	cin >> n;
	vector<string >colors(n);
	cout << "array color's length is " << colors.size();
	vector<int>weekdays;//�޳�Ա
	weekdays.push_back(2);//����һ�������Ա
	system("pause");
}
*/



/*   //03.array����
#include <iostream>
#include <array>
using namespace std;


int main()
{
	array <float, 4>score = { 68,66,104,101 };
	for (int i = 0; i < 4; i++)
	{
		cout << score[i] << '\t';
	}
	system("pause");
}
*/




/*    //limits���� numeric_limits<int>::max()=======================================================================
#include <iostream>
#include <limits>
using namespace std;

int main()
{
	cout << "Largest float==" << numeric_limits<float>::max() << ",char is signed ==" << numeric_limits<char >::is_signed << '\n';
	system("pause");
	return 0;
}
*/

/*  //��������========================================================
#include <iostream>
#include <string>
using namespace std;

int main()
{
	string a = "abcd";
	char b[] = "abdfsdg";
	int c = strlen("abcd");//4
	int d = strlen(b);//7
	int e = sizeof(a);//26
	int f = sizeof(b);//8
	int g = a.length();//4
	cout << c << endl << d << endl << e << endl << f << endl << g << endl;

	return 0;
}



#include <iostream>
#include <string>
using namespace std;

int main()
{
	char a[100] = "gcffcggcf";
	char*p = a;
	int d = sizeof(a);
	int b = strlen(a);
	cout << b << endl;
	cout << d << endl;
	return 0;
}
*/





/* //�ַ����Ǿ�̬�ģ�ֵ��ͬ����ַ��ͬ����int��ֵ��ͬ����ַҲ����ͬ
#include <iostream>
using namespace std;

int main()
{
	char*p = "123";
	char*q = "123";
	if (p == q)
		cout << "One!" << endl;
	int a = 3;
	int b = 3;
	int *Q = &a;
	int *P = &b;
	if (P == Q)
		cout << "Also one!" << endl;
	else cout << "Not one !" << endl; //yes
	return 0;
}
*/

/*
#pragma warning (disable : 4996)
#include<iostream>
using namespace std;

int main()
{
	char str[] = "mus|123|fef3|817u|fe|ufgo| ";
	char *p = NULL;
	p = strtok(str, "|");
	cout<<p<<endl;
	while((p = strtok(NULL, "|"))!=NULL)
	{
	cout<<p<<endl;
	}
	return 0;
 }
 */
 





/*
#include <iostream >
#include <fstream>
#include<string>
#include<vector>
using namespace std;

int main()
{
	vector<string >txt;
	string word;
	fstream file("F:\\java\\algorithms\\algs4-data\\1.txt");
	//file.open();
	if (!file.is_open())
	{
		cout << "�����޷����ļ���";
		exit(EXIT_FAILURE);
	}
	if (file.good())
	{
		while (file >> word)
		{
			txt.push_back(word);
			file >> word;
		}
		for (string a : txt)
		{
			cout << a << " ";
		}
		typedef vector<string>::size_type vec_sz;
		vec_sz size = txt.size();
		vec_sz cnt = 0;//same
		vector<vec_sz>count;//total

		string word2 = txt[0];
		vector<string>txt2;//unsame
		txt2.push_back(word2);
		for (vec_sz i = 0; i < size; i++)
		{
			if (word2 == txt[i])
				cnt++;
			else
			{
				count.push_back(cnt);
				txt2.push_back(txt[i]);
				cnt = 1;
			}
		}
	}
	if (file.eof())
		cout << "End of file!";
	else if (file.fail())
		cout << "REading terminated data mismatch.\n";
	return 0;
}
*/
 





/*
#include <iostream>
#include <fstream>
#include <vector>
#include <algorithm>
#include <string>

using namespace std;

int main()
{
	fstream file("F:\\java\\algorithms\\algs4-data\\1.txt");
	if (!file.is_open())
	{
		cout << "�����޷����ļ���";
		exit(EXIT_FAILURE);
	}
	string words;
	vector<string> texts;

	// ����ʽ����ĿǰΪֹ��texts���������еĵ�����
	while (file >> words && (words != "EOF"))
	{
		texts.push_back(words);
	}

	// ������ĵ�������
	sort(texts.begin(), texts.end());

	// ��ȡ�ܵĵ��ʸ���
	typedef vector<string>::size_type vec_sz;//vector�����ĳ���int
	vec_sz  size = texts.size();

	// ���ʳ��ִ���
	vec_sz  cnt;
	vector<vec_sz > count;

	// ���ظ��ĵ���
	string words2;
	vector<string> texts2;

	cnt = 0;
	words2 = texts[0];
	texts2.push_back(words2);

	for (vec_sz i = 0; i != size; i++)
	{
		if (words2 == texts[i])
		{
			cnt++;
		}
		else
		{
			count.push_back(cnt);

			words2 = texts[i];
			texts2.push_back(words2);

			cnt = 1;
		}
	}

	cout << "���ֵĵ��ʺͶ�Ӧ�Ĵ����ǣ�" << endl;
	int count_sz = count.size();
	for (vec_sz j = 0; j != count_sz; ++j)
	{
		cout << texts2[j] << " " << count[j] << "��" << endl;
	}

	return 0;
}
*/


 






/*  //����������
#include <iostream>
#include <string>
#include <map>
using namespace std;

int main() {
	typedef basic_string<wchar_t>wstring;
	wstring ws1, ws2;
	wcin.imbue(locale("chs"));
	wcout.imbue(locale("chs"));
	getline(wcin, ws1);
	getline(wcin, ws2);
	if (ws1.size() != ws2.size()) {
		wcout << ws1 << " �� " << ws2 << " ����Ӧ" << endl;
		return 1;
	}
	map<wchar_t, wchar_t> m1, m2;
	int n = ws1.size();
	for (int i = 0; i < n; i++) {
		if (m1.find(ws1[i]) == m1.end() && m2.find(ws2[i]) == m2.end()) {
			m1[ws1[i]] = ws2[i];
			m2[ws2[i]] = ws1[i];
		}
		else {
			if (m1[ws1[i]] != ws2[i] || m2[ws2[i]] != ws1[i]) {
				wcout << ws1 << L" �� " << ws2 << L" ����Ӧ" << endl;
				return 1;
			}
		}
	}
	wcout << ws1 << L" �� " << ws2 << L" ��Ӧ" << endl;
	return 0;
}
*/



/*
 #include <iostream>
 #include <string>
 using namespace std;
 int main()
 {
	char * a = "abvf";
	char b[] = "abcd";
	//a[2]='c';//errorֻ��ָ�벻�ɸģ����鶼���Ը�,����string�Ĺ��Ͳ�����
	cout << a[2] << endl;
	int i = strlen(a);//cout<<'\t'<<strlen(b)<<endl;//error
	int j = strlen("abcd");
	int k = sizeof(a) / sizeof(char); cout << k << '\t';
	int l = sizeof(b) / sizeof(b[0]); cout << l << endl;
	cout << i << endl;
	cout << j;
	cin.get();
	return 0;
}
*/




/*
#include <iostream>
#include<string>
using namespace std;

int main()
{
	int a[5] = { 1,2,3,4,5 };
	a[2] = 3;
	cout << a;
	return 0;
}
*/

#include<iostream>
using namespace std;

int main() {
	int a = 1, b = 2, c = 3;
	if (a > b) {
		if (a > c)
		{
			cout << a << endl;
		}
		else
		{
			cout << b << endl;
		}
	}
	else
		cout << c << endl;
	return 0;
}