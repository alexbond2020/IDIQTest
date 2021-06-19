import React, { Component} from 'react';
import { Header } from './components/Header';
import  ScrapedItem  from './components/ScrapedItem'
import './custom.css'


export default class App extends Component {
    static displayName = App.name;
        
    constructor(props) {
        super(props);
        this.state = {
            ScrapedItems: [],
            UrlsToScrap: '',
            StatusMessage: '',
            MaxItems : 10
        };
        this.handleScrap = this.handleScrap.bind(this);
        this.handleClear = this.handleClear.bind(this);
    }

    handleClear() {
        this.setState({ ScrapedItems: [], UrlsToScrap:''})
    }

    async handleScrap() {
        this.setState({ ScrapedItems: [], StatusMessage:'' })

        if (this.state.UrlsToScrap === '') {
            this.setState({ StatusMessage: "Provide urls to scrap" });
        }
        else {
            let maxItems = this.state.MaxItems;
            let urls = this.state.UrlsToScrap.trim().split(",");
            urls.slice(0, maxItems).forEach(url => this.sendRequest(url))
        }
    }

    async sendRequest(url) {
        if (url != '') {
            fetch('scrap?url=' + url)
                .then(response => response.json())
                .then(data => this.setState({
                    ScrapedItems: this.state.ScrapedItems.concat([data])
                }));
        }
    }

    handleChange(e) {
        this.setState({ UrlsToScrap: e.target.value})
    }

  render () {
    return (
        <div>
            <Header />
            <div className="layout">
                <form>
                    <label htmlFor="urlList">Type urls separated by comma to scrap (max {this.state.MaxItems})</label><br />
                    <textarea cols="65" value={this.state.UrlsToScrap} onChange={(e) => { this.handleChange(e) }} rows="4" name="urlList" id="urlList" /><br />
                    <input type="button" value="Scrap" onClick={this.handleScrap} id="submit" />
                    <input type="button" value="Clear" onClick={this.handleClear} id="clear" />
                    <span className="right">Loaded urls : {this.state.ScrapedItems.length}</span>
                </form>
                <div className="center red">{this.state.StatusMessage}</div>
                <hr/>
            </div>
            {this.state.ScrapedItems.map((i,index) => <ScrapedItem key={index} item={i} />)}
        </div>
    );
  }
}
