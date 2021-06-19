import React, { Component } from 'react';

export default class ScrapedItem extends Component {
    render() {
        let item = this.props.item
        let containerClassName = this._reactInternalFiber.index % 2 == 0
            ? "even"
            : "odd";
        
        let content = item.hasException
            ? <div className="exceptionContent">{this.props.item.content}</div>
            : <div className="scrapContent">{this.props.item.content}</div>;

        return (
            <div className={`${containerClassName} container`}>
                URL : <a target="_blank" className="url" href={`${item.url}`}>{item.url}</a>
                {content}
            </div>
        );
    }
}