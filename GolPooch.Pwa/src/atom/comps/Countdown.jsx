import React, { Component } from 'react'

export default class Countdown extends Component {
    constructor(props) {
        super(props);
        this.state = {
            minutes: props.minutes,
            seconds: props.seconds
        };
    }

    componentDidMount() {
       
        this.myInterval = setInterval(() => {
            const { seconds, minutes } = this.state
            if (seconds > 0) {
                this.setState(({ seconds }) => ({
                    seconds: seconds - 1
                }));

                if (seconds - 1 === 0) {
                    if (minutes === 0) {
                        this.props.done();
                        clearInterval(this.myInterval);
                    }
                }
            }
            if (seconds === 0) {
                if (minutes === 0) {
                    this.props.done();
                    clearInterval(this.myInterval);
                } else {
                    this.setState(({ minutes }) => ({
                        minutes: minutes - 1,
                        seconds: 59
                    }));
                }
            }
        }, 1000);
    }

    componentWillUnmount() {
        clearInterval(this.myInterval)
    }
     
    render() {
        const { minutes, seconds } = this.state
        return (
            <>
                {minutes === 0 && seconds === 0 ? <></> : <>{minutes}:{seconds < 10 ? `0${seconds}` : seconds}</>}
            </>
        )
    }
}