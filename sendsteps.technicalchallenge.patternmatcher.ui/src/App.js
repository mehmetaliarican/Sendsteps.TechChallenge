import logo from './logo.svg';
import './App.css';
import { useForm } from "react-hook-form";
import React, { useState } from 'react';

function App() {

  const [submitText, setSubmitText] = useState("Send");
  const [clearText, setClearText] = useState("Clear");
  const [occurences,updateOccurences] = useState([]);
  const [overlappings, updateOverlappings] = useState([]);

  const { register, handleSubmit, setValue, formState: { errors } } = useForm();
  const onSubmit = (data, e) => {
    let input = ([].slice.call(e.target.children)).map(s => s.children[0]).filter(i => i.type === "submit")[0];
    input.className = "sent";
    input.disabled = true;
    setSubmitText("Sent ✓");

    fetch("http://localhost:5000/patternmatching",
      {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
      })
      .then(res => res.json())
      .then(d => {
        if(d.occurences.length>0)
        {
          let items = []; 
          d.occurences.forEach((i)=> {
            console.log(i);
            i.occurences.map((o,index)=> items.push(<div key={index} className='occurence'>Word: {i.word} Character: {o.character}, Index: {o.index}</div>));
            items.push(<hr/>);
          });
          updateOccurences(items);
        }
        
        if(d.overlappings.length>0){
          let items = []; 
          d.overlappings.map((i,index)=> items.push(<div key={index} className='overlapping'>Word: {i.word}, Count: {i.overlappingCharacterCount}</div>));
          updateOverlappings(items);
        }

        setSubmitText("Send");
        input.className = "";
        input.disabled = false;
      })
      .catch(error => {
        console.error("Fetch error", error);
      });
  }
  const onClear = (e) => {
    setClearText("Cleared ✓")
    setValue('text', '');
    setValue('word', '');
    updateOccurences([]);
    updateOverlappings([]);
    setTimeout(() => {
      setClearText("Clear");
    }, 2000);
  }

  // const onError = (errors, e) => console.log(errors, e);

  return (
    <div className="App">
      <img src={logo} className="App-logo" alt="logo" />
      <header className="App-header">
        <div className='App-form-container'>
          <form onSubmit={handleSubmit((data, e) => onSubmit(data, e))} className="App-form">
            <span className="form-item">
              <label>Text</label>
              <textarea rows={10} {...register('text', { required: true })} ></textarea>

            </span>
            <span className="form-item">
              <label>Word</label>
              <input {...register('word', { required: true })} />

            </span>
            <span className="form-item">
              <input type="submit" value={submitText} />
            </span>
            <span className="form-item">
              <input type="button" value={clearText} onClick={onClear} />
            </span>
          </form>
        </div>
        <div className='App-result'>
          <div className='App-errors'>
            {errors.text && <span className="errorText">Text is required</span>}
            {errors.word && <span className='errorText'>A word is required.</span>}
            
          </div>
          <div className="App-data">
            {(!errors.text && !errors.word && occurences.length>0) && <div className='occurences'>Occurences:<hr/>{occurences}</div>}
            
            
            {(!errors.text && !errors.word && overlappings.length>0) && <div className='overlappings'>Overlappings:<hr/>{overlappings}</div>}
          </div>
        </div>
      </header>
    </div>
  );
}

export default App;
