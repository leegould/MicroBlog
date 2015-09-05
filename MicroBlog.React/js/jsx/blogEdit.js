var BlogEdit = React.createClass({
    render: function() {
        return (
            <form id="blogpostform">
                <h1>Enter the content for your new blogpost</h1>
                <div>
                    <label for="title"></label>
                    <input type="text" id="title" value="{ this.props.data.Title }" />
                </div>
                <div>
                    <div className="editor">
                        { this.props.data.content }
                    </div>
                </div>
                <div>
                    <input type="submit" value="Update" />
                    <a className="link" href="index.html">Home</a>
                </div>
            </form>
        );
}
});