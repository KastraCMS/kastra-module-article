import React, {Component} from 'react';
import SingleInput from '../components/singleinput';
import Message from '../components/message';
import { Link } from 'react-router-dom';
import Loading from '../components/loading'
import { getXSRFToken } from '../utils';
import CKEditor from '@ckeditor/ckeditor5-react';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';

class Article extends Component {
    constructor(props) {
        super(props);

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.closeErrorMessage = this.closeErrorMessage.bind(this);
        this.closeSuccessMessage = this.closeSuccessMessage.bind(this);

        this.state = {
            articleId: props.match.params.articleId || 0,
            moduleId: props.match.params.moduleId || 0,
            title: '',
            articleContent: '',
            imageUrl: '',
            articleOrder: 0,
            errors: [],
            displaySuccess: false,
            displayErrors: false,
            isLoading: false
        };
    }

    componentDidMount() {
        if (this.state.articleId > 0) {
            this.fetchArticle();
        }
    }

    fetchArticle() {
        let data = {};
        this.setState({ isLoading: true, loadingMessage: 'Loading article ...' });

        fetch(`/article/get/${this.state.articleId}`, 
        {
            method: 'GET',
            credentials: 'include',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'RequestVerificationToken' : getXSRFToken()
            }
        })
        .then(res => res.json())
        .then(
            (result) => {
                data.title = result.title;
                data.articleContent = result.articleContent;
                data.imageUrl = result.imageUrl;
                data.articleOrder = result.articleOrder;
                data.isLoading = false;

                this.setState(data);
            }
        ).catch(function(error) {
            this.setState({ isLoading: false });
            console.log('Error: \n', error);
        });
    }

    handleChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;
        
        this.setState({
            [name]: value,
            displaySuccess: false
        });
    }

    handleSubmit(event) {
        event.preventDefault();

        let newState = {};
        let data = {};
        data.articleId = this.state.articleId;
        data.title = this.state.title;
        data.articleContent = this.state.articleContent;
        data.imageUrl = this.state.imageUrl;
        data.articleOrder = this.state.articleOrder;
        data.moduleId = this.state.moduleId;

        this.setState({isLoading: true, loadingMessage: 'Saving article ...'});

        fetch(`/article/update`, 
        {
            method: 'POST',
            credentials: 'include',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'RequestVerificationToken' : getXSRFToken()
            },
            body: JSON.stringify(data)
        })
        .then(res => res.json())
        .then((result) => {
                newState.displayErrors = false;
                newState.displaySuccess = true;
                newState.isLoading = false;
                newState.articleId = result.articleId;

                this.setState(newState, this.fetchArticle());
            }
        ).catch(function(error) {
            this.setState({ isLoading: false });
            console.log('Error: \n', error);
        });
    }

    closeSuccessMessage() {
        this.setState({ displaySuccess: false });
    }

    closeErrorMessage() {
        this.setState({ displayErrors: false });
    }

    render() {
        const articleTitle = (this.state.articleId > 0) ? `Edit article : ${this.state.title}` : 'New article';
        return (
            <div className="text-white m-sm-5 p-5 bg-dark clearfix">
                <Link to={`/admin/module/settings/${this.state.moduleId}/settings`} className="btn btn-outline-info mb-4">Back</Link>
                <Loading isLoading={this.state.isLoading} message={this.state.loadingMessage} />
                <h4 className="text-center">Edit the article</h4>
                <hr/>
                <h2 className="mb-5 text-center">{articleTitle}</h2>

                <Message display={this.state.displaySuccess} handleClose={this.closeSuccessMessage} type="success" message="Article updated with success" />
                <Message display={this.state.displayErrors} handleClose={this.closeErrorMessage} type="danger" messages={this.state.errors} />

                <form onSubmit={this.handleSubmit}>
                    <SingleInput type="text" handleChange={this.handleChange} title="Title" name="title" value={this.state.title} />
                    
                    <div className="form-group row">
                        <label htmlFor="articleContent" className="col-sm-2 col-form-label">Content</label>
                        <div className="col-sm-10">
                            <CKEditor
                                editor={ ClassicEditor }
                                data={this.state.articleContent}
                                onChange={ ( event, editor ) => {
                                    const data = editor.getData();
                                    this.setState({ articleContent: data });
                                } }
                            />                        
                        </div>
                    </div>
                    <SingleInput type="text" handleChange={this.handleChange} title="Image url" name="imageUrl" value={this.state.imageUrl} />
                    <SingleInput type="text" handleChange={this.handleChange} title="Order" name="articleOrder" value={this.state.articleOrder} />
                    
                    <button className="btn btn-outline-info mt-5 float-right" type="submit">Valid</button>
                </form>
            </div>
        );
    }
}

export default Article;