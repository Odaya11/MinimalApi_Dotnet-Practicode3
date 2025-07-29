import axios from 'axios';
//ניתוב ברירת מחדל
axios.defaults.baseURL = 'http://localhost:5283';

//תפיסת שגיאות והדפסןת בלוג
axios.interceptors.response.use( (response)=> {
  //חוזר אם זה תקין
   console.log("OK");
   return response;
},
  (error)=> {
  //אם זה לא תקין מחזיר את השגיאה
  console.log("the error:"+error);
  
  return Promise.reject(error);
});
export default {
  //הצגת המשימות
  getTasks: async () => {
    console.log("i am in the get")
    console.log(`/getAll`);
    
    const result = await axios.get(`/`)
    
    console.log(result.data);
    
    return result.data;
  },
// הוספת משימה 
  addTask: async (name) => {
    console.log('addTask', name)
    console.log(`/add/${name}`)
    const result = await axios.post(`/${name}`)
    console.log("after log")
    // return result.data;
    return{};
  },
//עדכון משימה
  setCompleted: async (id, isComplete) => {
    console.log('setCompleted', { id, isComplete })
    console.log(`/${id}/${isComplete}`);
    
    const result = await axios.put(`/${id}/${isComplete}`)
console.log("after");

    return {};
  },
//מחיקת משימה מהמערכת
  deleteTask: async (id) => {
    console.log('deleteTask')
    console.log(`/${id}`);
    
    const result = await axios.delete(`/del/${id}`)

  }
  
};
