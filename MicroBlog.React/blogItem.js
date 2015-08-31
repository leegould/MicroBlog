var BlogItems = React.createClass({
    render: function() {
        var blogItems = this.props.data.map(function (blog) {
            return (
                <div className="row">
                    <div className="blogheader">
                        <a href={ 'view.html?id=' + blog.id }>
                            <h1>{ blog.title }</h1>
                        </a>
                    </div>
                    <span dangerouslySetInnerHTML={{__html: blog.content}} />
                </div>
            );
        });
        return (
            <div>
                {blogItems}
            </div>
        );
    }
});