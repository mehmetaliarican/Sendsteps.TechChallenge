import './match_form.css';
import { useForm } from "react-hook-form";
import React, { Fragment, useState } from 'react';
import { useAlert } from "react-alert";
import { MatchingService } from '../services/matching_service'

function RenderMatchInputs(events) {

    const service = new MatchingService();
    const alert = useAlert();

    const { register, handleSubmit, setValue, formState: { errors } } = useForm();

    const [submitText, setSubmitText] = useState("Send");
    const [clearText, setClearText] = useState("Clear");


    const onSubmit = (formData, e) => {
        let input = ([].slice.call(e.target.children)).map(s => s.children[0]).filter(i => i.type === "submit")[0];
        input.className = "sent";
        input.disabled = true;
        setSubmitText("Sent ✓");
        service.fetchResultsAsync(formData)
            .then(res => {
                if (res && res.data && res.data.length > 0) {
                    let items = [];
                    res.data.forEach((v, index) => {
                        items.push(
                            <div
                                key={index}
                                className='result-item'>
                                {v.icon} {v.primary}+{v.secondary} {'=>'} {v.conclusion} {`(${v.expected})`}
                            </div>);
                        items.push(<hr />);
                    });
                    events.onUpdateResultItems(items);
                }
                else{
                    alert.error(res.message)
                }
                setSubmitText("Send");
                input.className = "";
                input.disabled = false;
            })
    }

    const onClear = (e) => {
        setClearText("Cleared ✓")
        setValue('primary', '');
        setValue('secondary', '');
        events.onUpdateResultItems([]);
        setTimeout(() => {
            setClearText("Clear");
        }, 1200);
    }

    return (
        <Fragment>

            <div className='App-form-container'>
                <form onSubmit={handleSubmit((data, e) => onSubmit(data, e))} className="App-form">
                    <span className="form-item">
                        <label>Primary:</label>
                        <input {...register('primary', { required: true })} />

                    </span>
                    <span className="form-item">
                        <label>Secondary:</label>
                        <input {...register('secondary', { required: true })} />

                    </span>
                    <span className="form-item">
                        <input type="submit" value={submitText} />
                    </span>
                    <span className="form-item">
                        <input type="button" value={clearText} onClick={onClear} />
                    </span>
                </form>
                <div className='App-errors'>
                    {errors.primary && <span className="errorText">A primary value is required</span>}
                    {errors.secondary && <span className='errorText'>A secondary value is required.</span>}
                </div>
            </div>
        </Fragment>
    );
}

export default RenderMatchInputs;