var BlogItem = React.createClass({
    render: function() {
        return (
            <div className="row">
                <div className="blogheader">
                    <a href={ 'view.html?id=' + this.props.data.id }>
                        <h1>{ this.props.data.title }</h1>
                    </a>
                </div>
                <span dangerouslySetInnerHTML={{__html: this.props.data.content}} />
            </div>
        );
    }
});