using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomPlayerNamePicker : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshPro text;
    private string[] names = { "Aaron", "Abraham", "Adam", "Alan", "Alec", "Alex", "Alexander", "Amos", "Andrew", "Anthony", "Andy", "Arnold", "Arthur", "Avery", "Barry", "Bart", "Bartholomew", "Benjamin", "Bernard", "Benny", "Bill", "Bob", "Bobby", "Brad", "Bradley", "Brent", "Brett", "Brian", "Bruce", "Burt", "Byron", "Caleb", "Calvin", "Carl", "Chad", "Chadwick", "Chandler", "Charles", "Charlie", "Chris", "Christopher", "Chuck", "Clark", "Cliff", "Cole", "Colin", "Cory", "Dale", "Daniel", "Darrel", "Darren", "Dave", "David", "Dennis", "Derek", "Devin", "Dick", "Dirk", "Donald", "Donny", "Doug", "Douglas", "Drake", "Drew", "Dwain", "Dwight", "Edgar", "Edward", "Edwin", "Elliot", "Eric", "Ernest", "Ernie", "Frank", "Franklin", "Fred", "Frederick", "Gary", "George", "Geoffrey", "Glenn", "Gordon", "Graham", "Grant", "Greg", "Gregory", "Harold", "Harry", "Harvey", "Heidi", "Henry", "Howard", "Hunter", "Ichabod", "Isaac", "Ivan", "Jack", "James", "Jason", "Jay", "Jeff", "Jeremy", "Jerome", "Jim", "Jimmy", "John", "Jonathan", "Jordan", "Joe", "Joel", "Joseph", "Joshua", "Justin", "Keith", "Kareem", "Kenneth", "Kevin", "Kirk", "Kurt", "Kyle", "Lance", "Larry", "Lawrence", "Lee", "Leonard", "Lester", "Lionel", "Llewellyn", "Lou", "Louis", "Lyle", "Mark", "Mashall", "Martin", "Marty", "Marvin", "Matt", "Matthew", "Maurice", "Michael", "Nathan", "Nathaniel", "Ned", "Neil", "Nicholas", "Nick", "Nolan", "Norbert", "Norman", "Oliver", "Oscar", "Oswald", "Patrick", "Paul", "Peter", "Phillip", "Quincy", "Ralph", "Randy", "Ray", "Raymond", "Reuben", "Richard", "Rick", "Ricky", "Robbie", "Robert", "Robin", "Rodney", "Roger", "Ron", "Ronald", "Ronnie", "Russell", "Rusty", "Ryan", "Sam", "Samuel", "Scott", "Shawn", "Sheldon", "Sidney", "Simon", "Spencer", "Stanley", "Steve", "Steven", "Stewart", "Taylor", "Ted", "Theodore", "Thomas", "Tim", "Timothy", "Todd", "Tom", "Tommy", "Tony", "Travis", "Trent", "Trevor", "Tyler", "Ulysses", "Victor", "Wallace", "Wally", "Ward", "Wayne", "Wendall", "William", "Xavier", "Yancy" };
    void Start()
    {
        GetComponent<TMP_InputField>().SetTextWithoutNotify(names[Random.Range(0, names.Length-1)]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
