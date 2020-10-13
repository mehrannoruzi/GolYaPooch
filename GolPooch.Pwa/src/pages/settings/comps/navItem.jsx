import React from 'react';
import { Link } from 'react-router-dom';
import { Container } from '@material-ui/core';
import { FiChevronLeft } from 'react-icons/fi';
const Item = (props) => {

    return (
        <li>
            <Container>
                {props.onClick ? <button onClick={() => props.onClick()} className='item'>
                    <props.icon className='icon' />
                    <h5 className='hx'>{props.title}</h5>
                    <FiChevronLeft className='arrow-left' />
                </button> :
                    <Link to={props.href}  className='item'>
                        <props.icon className='icon' />
                        <h5 className='hx'>{props.title}</h5>
                        <FiChevronLeft className='arrow-left' />
                    </Link>}

            </Container>
        </li>
    );
}
export default Item;