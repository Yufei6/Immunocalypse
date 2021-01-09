using System.Collections;
using System.Collections.Generic;
using static System.Convert;
using System.IO;
using UnityEngine;
using FYFY;

public class LireFicherSystem : FSystem {
	private Family map = FamilyManager.getFamily(
		new AllOfComponents(
			typeof(Map),typeof(ObjetMap)
			));
    private Family prefabfac = FamilyManager.getFamily(
        new AllOfComponents(
            typeof(factoryObjet)
            ));
	private Map map1;
    private TimeLine tl;
    private List<string> paths=new List<string>();
    private int factory=0;
    private List<Vector3> pfactory=new List<Vector3>();
    private GameObject prefab;
    private string text="Assets/selfmake/allpath.txt";
    private Family timeline=FamilyManager.getFamily(new AllOfComponents(typeof(TimeLine)), new NoneOfComponents(typeof(TimelineEvent)));
	public LireFicherSystem(){
		lireAllpath(text);

        tl=timeline.First().GetComponent<TimeLine>();
		map1=map.First().GetComponent<Map>();
        prefab=prefabfac.First().GetComponent<factoryObjet>().fac;
        string path=(string)paths[0];
        liremap(map1,path);
        lirefactory();
        liretimeline();


	}
	private void lireAllpath(string path){
		try
        {
            string[] lines = File.ReadAllLines(path);
            Debug.Log(lines);
            foreach( string str in lines){
              paths.Add(str);  
            }
            
        }
        catch(IOException)
        {
            Debug.Log("An IO exception has been thrown!");
        }
	}
	private void liremap(Map map1,string path){
		try
        {
            string[] lines = File.ReadAllLines(path);
            string[] words = lines[0].Split(',');
            map1.rout1=new int[12];
            map1.rout2=new int[12];
            map1.rout3=new int[12];
            map1.rout4=new int[12];
            map1.rout5=new int[12];
            map1.rout6=new int[12];
            int count=0;
            foreach( string word in words){
            	map1.rout1[count]=ToInt32(word);
                if(map1.rout1[count]==0){
                    factory+=1;
                    pfactory.Add(new Vector3(-6+count,3,0));
                }
            	count +=1;
            }
            count=0;
            words=lines[1].Split(',');
            foreach( string word in words){
            	map1.rout2[count]=ToInt32(word);
                if(map1.rout2[count]==0){
                    factory+=1;
                    pfactory.Add(new Vector3(-6+count,2,0));
                }
            	count +=1;
            }
            count=0;
            words=lines[2].Split(',');
            foreach( string word in words){
            	map1.rout3[count]=ToInt32(word);
                if(map1.rout3[count]==0){
                    factory+=1;
                    pfactory.Add(new Vector3(-6+count,1,0));
                }
            	count +=1;
            }
            count=0;
            words=lines[3].Split(',');
            foreach( string word in words){
            	map1.rout4[count]=ToInt32(word);
                if(map1.rout4[count]==0){
                    factory+=1;
                    pfactory.Add(new Vector3(-6+count,0,0));
                }
            	count +=1;
            }
            count=0;
            words=lines[4].Split(',');
            foreach( string word in words){
            	map1.rout5[count]=ToInt32(word);
                if(map1.rout5[count]==0){
                    factory+=1;
                    pfactory.Add(new Vector3(-6+count,-1,0));
                }
            	count +=1;
            }
            count=0;
            words=lines[5].Split(',');
            foreach( string word in words){
            	map1.rout6[count]=ToInt32(word);
                if(map1.rout6[count]==0){
                    factory+=1;
                    pfactory.Add(new Vector3(-6+count,-2,0));
                }
            	count +=1;
            }
        }
        catch(IOException)
        {
            Debug.Log("An IO exception has been thrown!");
        }

	}
    private void lirefactory(){
        for(int i=1; i<factory+1; i++){
            GameObject ft=Object.Instantiate<GameObject>(prefab,pfactory[i-1],Quaternion.identity);
            ft.GetComponent<ID>().id=i;
            try
            {
                string[] lines = File.ReadAllLines(paths[i]);
                foreach( string str in lines){
                  string[] words=str.Split(',');
                  int x =ToInt32(words[0]);
                  int y =ToInt32(words[1]);
                  int z =ToInt32(words[2]);
                  ft.GetComponent<Routine>().routine.Add(new Vector3(x,y,z));
                }
                
            }
            catch(IOException)
            {
                Debug.Log("An IO exception has been thrown!");
            }
                        

        }
    }

    private void liretimeline(){
        try
        {
            string[] lines = File.ReadAllLines(paths[factory+1]);
            string[] words=lines[0].Split(',');
            int l= words.Length;
            tl.frame= new int[l+1];
            for(int i=0;i<l;i++){
                tl.frame[i]=ToInt32(words[i]);
            }
            tl.frame[l]=0;
            words=lines[1].Split(',');
            l= words.Length;
            tl.type_enemy= new int[l];
            for(int i=0;i<l;i++){
                tl.type_enemy[i]=ToInt32(words[i]);
            }
            words=lines[2].Split(',');
            l= words.Length;
            tl.id_fac= new int[l];
            for(int i=0;i<l;i++){
                tl.id_fac[i]=ToInt32(words[i]);
            }
            tl.win_condtion=l;
              
            
        }
        catch(IOException)
        {
            Debug.Log("An IO exception has been thrown!");
        }
                
    }
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
	}
}