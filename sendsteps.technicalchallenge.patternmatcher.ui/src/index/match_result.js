import './match_form.css';

function RenderMatchResults(result) {
  return (
    <div className='App-result'>

      <div className="App-data">
        {(result.data.length > 0) && <div key="data-item" className='result-items'>{result.data}</div>}

      </div>
    </div>
  );
}

export default RenderMatchResults