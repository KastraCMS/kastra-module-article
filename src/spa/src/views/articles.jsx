import React, {Component} from 'react';
import { Link } from 'react-router-dom';
import ConfirmDialog from '../components/confirmdialog';
import Loading from '../components/loading'
import { getXSRFToken } from '../utils';

class Articles extends Component {
    constructor(props) {
        super(props);

        this.state = {
            moduleId: props.match.params.moduleId || 0,
            isLoading: false,
            loadingMessage: '',
            articles: []
        };
    }

    componentDidMount() {
        this.loadArticles();
    }

    loadArticles() {
        this.setState({ isLoading: true, loadingMessage: 'Loading articles ...' });

        fetch(`/article/list/${this.state.moduleId}`, 
        {
            method: 'GET',
            credentials: 'include'
        })
        .then(res => res.json())
        .then(
            (result) => {
                this.setState({
                    articles: result,
                    isLoading: false
                });
            }
        ).catch(function(error) {
            this.setState({ isLoading: false });
            console.log('Error: \n', error);
        });
    }

    handleDelete(id) {
        this.setState({ isLoading: true, loadingMessage: 'Deleting article ...' });

        fetch('/article/delete', 
        {
            method: 'DELETE',
            credentials: 'include',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'RequestVerificationToken' : getXSRFToken()
            },
            body: JSON.stringify(id)
        })
        .then(
            () => {
                this.loadArticles();
            }
        ).catch(function(error) {
            this.setState({ isLoading: false });
            console.log('Error: \n', error);
        });
    }

    renderArticles() {
        return this.state.articles.map((article, index) => {
            const dialogId = `dialog-${index}`;
            return (
                <tr key={index}>
                    <td>{article.title}</td>
                    <td>{article.dateUpdated}</td>
                    <td><Link to={`/admin/module/settings/${this.state.moduleId}/settings/article/${article.articleId}`}><span className="ion-compose"></span></Link></td>
                    <td>
                        <a href="" onClick={(e) => e.preventDefault()} data-toggle="modal" data-target={`#${dialogId}`}><span className="ion-trash-a"></span></a>
                        <ConfirmDialog id={dialogId} 
                            title="Delete article"
                            message={`Are you sure that you want to delete the article ${article.title} ?`}
                            onConfirm={() => this.handleDelete(article.articleId)}
                            confirmLabel="Delete"
                            cancelLabel="Cancel" />
                    </td>
                </tr>
            );
        });
    }

    render() {
        return (
            <div className="text-white m-sm-5 p-5 bg-dark clearfix">
                <Loading isLoading={this.state.isLoading} message={this.state.loadingMessage} />
                <h2 className="mb-5 text-center">Articles</h2>
                <Link to={`/admin/module/settings/${this.state.moduleId}/settings/article/`} className="btn btn-outline-info mb-4">New</Link>
                <table className="table table-dark bg-dark">
                    <thead>
                        <tr>
                            <th scope="col">Name</th>
                            <th scope="col">Date</th>
                            <th scope="col">Edit</th>
                            <th scope="col">Delete</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.renderArticles()}
                    </tbody>
                </table>
            </div>
        );
    }
}

export default Articles;