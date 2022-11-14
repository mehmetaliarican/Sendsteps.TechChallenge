import logo from './logo.svg';
import './App.css';
import React, { useState, useEffect } from 'react';
import RenderMatchResults from './index/match_result';
import RenderMatchInputs from './index/match_form';

function App() {

  const [resultData,updateResultItems] = useState([]);
   useEffect(()=>{
    document.title = 'Sendsteps';
   });


  return (
    <div className="App">
      <img src={logo} className="App-logo" alt="logo" />
      <header className="App-header">

        <RenderMatchInputs 
              onUpdateResultItems={updateResultItems}/>

        <RenderMatchResults data={resultData} />
           
      </header>
    </div>
  );
}

export default App;


